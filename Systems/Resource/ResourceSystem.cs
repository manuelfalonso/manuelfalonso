using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Systems.Resource
{
    /// <summary>
    /// Supports:
    /// - Increase and decrease resource amount
    /// - Restore resource to a defined amount
    /// - Lock amount cooldowns when resource increase and decrease 
    /// - Resource immutable behaviour
    /// - Events:
    /// AmountChanged
    /// AmountRestored
    /// AmountMaxed
    /// AmountLow
    /// AmountEmptied
    /// 
    /// It can be used with any tipe of resource. For example life, mana, wood, water, etc.
    /// </summary>
    public class ResourceSystem : MonoBehaviour
    {
        [Header("ResourceBase")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        [Header("Resource")]
        [SerializeField] private float _initialAmount;
        [SerializeField] private float _maxAmount;

        [Header("Protection cooldown")]
        [Tooltip("In seconds")]
        [SerializeField] private float _increaseCooldown = 0f;
        [Tooltip("In seconds")]
        [SerializeField] private float _decreaseCooldown = 0f;

        [Header("Regeneration")]
        [Tooltip("In seconds")]
        [SerializeField] private float _regenerationTick = 0f;
        [Tooltip("Per tick")]
        [SerializeField] private float _regenerationRate = 0;
        [Tooltip("In seconds")]
        [SerializeField] private float _degenerationTick = 0f;
        [Tooltip("Per tick")]
        [SerializeField] private float _degenerationRate = 0;

        [Header("Low resource")]
        [Tooltip("If the resource is lower than this value the OnLowResource event will be invoked")]
        [SerializeField] private float _lowAmount = 0;

        public float LowAmount { get; set; }
        public float IncreaseCooldownInSeconds { get; private set; }
        public float DecreaseCooldownInSeconds { get; private set; }
        public float RegeneratationRateInSeconds { get; private set; }
        public float RegnerationTick { get; private set; }
        public float DegenerattionRateInSeconds { get; private set; }
        public float DegenerattionTick { get; private set; }

        public bool Immutable { get; set; }
        public bool IsLowAmount { get; private set; } = false;
        public bool IsInIncreaseCooldown { get; private set; } = false;
        public bool IsInDecreaseCooldown { get; private set; } = false;

        public bool IsEmptyAmount => _resource.Amount == 0;
        public bool IsMaxAmount => _resource.Amount == _resource.MaxAmount;

        public float Amount => _resource.Amount;
        public float MaxAmount => _resource.MaxAmount;
        public float ResourcePercent => (float)_resource.Amount / (float)_resource.MaxAmount;

        private Resource _resource = null;
        private WaitForSeconds _increaseWaitForSeconds;
        private WaitForSeconds _decreaseWaitForSeconds;
        private WaitForSeconds _regenerationWaitForSeconds;
        private WaitForSeconds _degenerationWaitForSeconds;
        private ResourceSystemData _currentData = new ResourceSystemData();
        private ResourceSystemResult _currentResult = new ResourceSystemResult();

        public UnityEvent AmountChanged = new UnityEvent();
        public UnityEvent AmountRestored = new UnityEvent();
        public UnityEvent AmountMaxed = new UnityEvent();
        public UnityEvent AmountLow = new UnityEvent();
        public UnityEvent AmountEmptied = new UnityEvent();


        private void Awake()
        {
            InitialSetup();
        }

        private void OnEnable()
        {
            StartCoroutine(Regeneration());
            StartCoroutine(Degeneration());
        }

        private void OnDisable()
        {
            RestoreCooldownsOnDisable();
            StopCoroutine(Regeneration());
            StopCoroutine(Degeneration());
        }


        /// <summary>
        /// The Resource must not be empty. Call RestoreAmount or ResetAmount instead
        /// </summary>
        /// <param name="amountToIncrease">Must be greater than 0</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ResourceSystemResult IncreaseAmount(float amountToIncrease)
        {
            if (!IncreaseResource(amountToIncrease, true)) { return null; }

            StartCoroutine(IncreaseCooldown());

            return SetResultData();
        }

        /// <param name="amountToDecrease">Must be greater than 0</param>
        public ResourceSystemResult DecreaseAmount(float amountToDecrease)
        {
            if (!DecreaseResource(amountToDecrease, true)) { return null; }

            StartCoroutine(DecreaseCooldown());

            return SetResultData();
        }

        /// <summary>
        /// Restores resource amount only if the resource was empty.
        /// </summary>
        /// <param name="restorePercentage">Percentage restore amount of total Resource</param>
        public ResourceSystemResult RestoreAmount(float restorePercentage = 1f)
        {
            if (!IsEmptyAmount) { return null; }
            if (Immutable) { return null; }

            if (restorePercentage < 0f || restorePercentage > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(restorePercentage));
            }

            _resource.IncreaseAmount(_resource.MaxAmount * restorePercentage);

            OnResourceChanged();
            OnResourceRestored();

            return SetResultData();
        }

        /// <summary>
        /// Reset Resource to initial amount only if the resource was empty.
        /// </summary>
        public ResourceSystemResult ResetAmount()
        {
            if (!IsEmptyAmount) { return null; }
            if (Immutable) { return null; }

            _resource.ResetAmount();

            OnResourceChanged();
            OnResourceRestored();

            return SetResultData();
        }

        /// <summary>
        /// Set the Resource to 0
        /// </summary>
        public ResourceSystemResult ClearAmount()
        {
            if (IsEmptyAmount) { return null; }
            if (Immutable) { return null; }

            _resource.ClearAmount();

            OnResourceEmptied();

            return SetResultData();
        }


        private void InitialSetup()
        {
            var resource = new Resource(_name, _description, _initialAmount, _maxAmount);
            _resource = resource;

            _increaseWaitForSeconds = new WaitForSeconds(_increaseCooldown);
            _decreaseWaitForSeconds = new WaitForSeconds(_decreaseCooldown);
            _regenerationWaitForSeconds = new WaitForSeconds(_regenerationTick);
            _degenerationWaitForSeconds = new WaitForSeconds(_degenerationTick);
        }

        private bool IncreaseResource(float amountToIncrease, bool considerCooldowns)
        {
            var success = false;

            // Validate amount
            if (amountToIncrease <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountToIncrease));
            }

            if (IsEmptyAmount) { return success; }
            if (Immutable) { return success; }
            if (considerCooldowns)
            {
                if (IsInIncreaseCooldown) { return success; }
            }

            // Apply amount
            _resource.IncreaseAmount(amountToIncrease);
            OnResourceChanged();

            // Validate OnLowResource
            if (_resource.Amount > LowAmount && IsLowAmount)
            {
                IsLowAmount = false;
            }

            if (IsMaxAmount)
            {
                OnResourceMaxed();
            }

            success = true;

            return success;
        }

        private bool DecreaseResource(float amountToDecrease, bool considerCooldowns)
        {
            var success = false;

            // Validate amount
            if (amountToDecrease <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountToDecrease));
            }

            if (IsEmptyAmount) { return success; }
            if (Immutable) { return success; }
            if (considerCooldowns)
            {
                if (IsInDecreaseCooldown) { return success; }
            }

            // Apply amount
            _resource.DecreaseAmount(amountToDecrease);
            OnResourceChanged();

            // Validate OnLowResource
            if (_resource.Amount < LowAmount && !IsLowAmount)
            {
                IsLowAmount = true;
                OnResourceLow();
            }

            if (IsEmptyAmount)
            {
                OnResourceEmptied();
            }

            return success;
        }

        private void RestoreCooldownsOnDisable()
        {
            if (IsInDecreaseCooldown)
            {
                IsInDecreaseCooldown = false;
            }

            if (IsInIncreaseCooldown)
            {
                IsInIncreaseCooldown = false;
            }
        }

        private ResourceSystemResult SetResultData()
        {
            var currentData = new ResourceSystemData()
            {
                Amount = Amount,
                MaxAmount = MaxAmount,
                Percent = ResourcePercent,
                IsMaxAmount = IsMaxAmount,
                IsLowAmount = IsLowAmount,
                IsEmptyAmount = IsEmptyAmount,
                Immutable = Immutable,
                IsInCooldown = IsInDecreaseCooldown || IsInIncreaseCooldown
            };

            _currentResult.PreviousData = _currentData;
            _currentData = currentData;
            _currentResult.CurrentData = currentData;

            return _currentResult;
        }

        #region Events
        private void OnResourceChanged()
        {
            AmountChanged?.Invoke();
        }

        private void OnResourceRestored()
        {
            AmountRestored?.Invoke();
        }

        private void OnResourceMaxed()
        {
            AmountMaxed?.Invoke();
        }

        private void OnResourceLow()
        {
            AmountLow?.Invoke();
        }

        private void OnResourceEmptied()
        {
            AmountEmptied?.Invoke();
        }
        #endregion

        #region Coroutines
        private IEnumerator Regeneration()
        {
            if (_regenerationTick == 0f) { yield break; }

            while (true)
            {
                yield return _regenerationWaitForSeconds;

                IncreaseResource(_regenerationRate, false);
            }
        }

        private IEnumerator Degeneration()
        {
            if (_degenerationTick == 0f) { yield break; }

            while (true)
            {
                yield return _degenerationWaitForSeconds;

                // Validate amount
                if (_degenerationRate <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(_degenerationRate));
                }

                if (IsEmptyAmount) { yield return null; }
                if (Immutable) { yield return null; }

                // Apply amount
                _resource.DecreaseAmount(_degenerationRate);
                OnResourceChanged();

                // Validate OnLowResource
                if (_resource.Amount < LowAmount && !IsLowAmount)
                {
                    IsLowAmount = true;
                    OnResourceLow();
                }

                if (IsEmptyAmount)
                {
                    OnResourceEmptied();
                }
            }
        }

        private IEnumerator IncreaseCooldown()
        {
            if (_increaseCooldown == 0f) { yield break; }

            IsInIncreaseCooldown = true;
            yield return _increaseWaitForSeconds;
            IsInIncreaseCooldown = false;
            yield break;
        }

        private IEnumerator DecreaseCooldown()
        {
            if (_decreaseCooldown == 0f) { yield break; }

            IsInDecreaseCooldown = true;
            yield return _decreaseWaitForSeconds;
            IsInDecreaseCooldown = false;
            yield break;
        }
        #endregion
    }

    public class ResourceSystemResult
    {
        public ResourceSystemData PreviousData;
        public ResourceSystemData CurrentData;
    }
}
