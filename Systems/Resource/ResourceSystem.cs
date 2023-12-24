using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Supports:
    /// - Increase and decrease resource amount
    /// - Restore resource to a defined amount
    /// - Lock amount with cooldowns when resource increase and decrease 
    /// - Resource immutable behaviour
    /// - Regeneration and degeneration
    /// - Events:
    /// AmountChanged
    /// AmountChangedNormalized
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
        /// <summary>
        /// The name of the resource.
        /// </summary>
        [Tooltip("The name of the resource.")]
        [SerializeField] private string _name;
        /// <summary>
        /// The description of the resource.
        /// </summary>
        [Tooltip("The description of the resource.")]
        [SerializeField] private string _description;

        [Header("Resource")]
        /// <summary>
        /// The initial amount of the resource.
        /// </summary>
        [Tooltip("The initial amount of the resource.")]
        [SerializeField] private float _initialAmount;
        /// <summary>
        /// The maximum amount the resource can have.
        /// </summary>
        [Tooltip("The description of the resource.")]
        [SerializeField] private float _maxAmount;

        [Header("Protection cooldown")]
        /// <summary>
        /// Cooldown time for protection against increasing the resource amount, in seconds.
        /// </summary>
        [Tooltip("Cooldown time for protection against increasing the resource amount, in seconds.")]
        [SerializeField] private float _increaseCooldownTime = 0f;
        /// <summary>
        /// Cooldown time for protection against decreasing the resource amount, in seconds.
        /// </summary>
        [Tooltip("Cooldown time for protection against decreasing the resource amount, in seconds.")]
        [SerializeField] private float _decreaseCooldownTime = 0f;

        [Header("Regeneration")]
        /// <summary>
        /// Determines if the resource regenerates.
        /// </summary>
        [Tooltip("Determines if the resource regenerates.")]
        [SerializeField] private bool _regenerate = true;
        /// <summary>
        /// Time between regeneration ticks, in seconds.
        /// </summary>
        [Tooltip("Time between regeneration ticks, in seconds.")]
        [SerializeField] private float _regenerationTick = 0f;
        /// <summary>
        /// Rate of resource regeneration per tick.
        /// </summary>
        [Tooltip("Rate of resource regeneration per tick.")]
        [SerializeField] private float _regenerationRate = 0;
        /// <summary>
        /// Determines if the resource degenerates.
        /// </summary>
        [Tooltip("Determines if the resource degenerates.")]
        [SerializeField] private bool _degenerate = true;
        /// <summary>
        /// Time between degeneration ticks, in seconds.
        /// </summary>
        [Tooltip("Time between degeneration ticks, in seconds.")]
        [SerializeField] private float _degenerationTick = 0f;
        /// <summary>
        /// Rate of resource degeneration per tick.
        /// </summary>
        [Tooltip("Rate of resource degeneration per tick.")]
        [SerializeField] private float _degenerationRate = 0;

        [Header("Low resource")]
        /// <summary>
        /// If the resource is lower than this value, the OnLowResource event will be invoked.
        /// </summary>
        [Tooltip("If the resource is lower than this value the OnLowResource event will be invoked")]
        [SerializeField] private float _lowAmount = 0;

        /// <summary>
        /// Gets or sets a value indicating whether the resource is immutable.
        /// </summary>
        public bool Immutable { get; set; }
        /// <summary>
        /// Gets a value indicating whether the resource is in a low amount state.
        /// </summary>
        public bool IsLowAmount { get; private set; } = false;
        /// <summary>
        /// Gets a value indicating whether the resource is in an increase cooldown state.
        /// </summary>
        public bool IsInIncreaseCooldown { get; private set; } = false;
        /// <summary>
        /// Gets a value indicating whether the resource is in a decrease cooldown state.
        /// </summary>
        public bool IsInDecreaseCooldown { get; private set; } = false;

        /// <summary>
        /// Gets a value indicating whether the resource is empty.
        /// </summary>
        public bool IsEmptyAmount => _resource.Amount == 0;
        /// <summary>
        /// Gets a value indicating whether the resource is at its maximum amount.
        /// </summary>
        public bool IsMaxAmount => _resource.Amount == _resource.MaxAmount;

        /// <summary>
        /// Gets the current resource amount.
        /// </summary>
        public float Amount => _resource.Amount;
        /// <summary>
        /// Gets the maximum resource amount.
        /// </summary>
        public float MaxAmount => _resource.MaxAmount;
        /// <summary>
        /// Gets the resource percentage.
        /// </summary>
        public float ResourcePercent => (float)_resource.Amount / (float)_resource.MaxAmount;

        /// <summary>
        /// Gets or sets the cooldown time for resource increase.
        /// </summary>
        public float IncreaseCooldownTime { get => _increaseCooldownTime; set => _increaseCooldownTime = value; }
        /// <summary>
        /// Gets or sets the cooldown time for resource decrease.
        /// </summary>
        public float DecreaseCooldownTime { get => _decreaseCooldownTime; set => _decreaseCooldownTime = value; }
        /// <summary>
        /// Gets or sets a value indicating whether the resource regenerates.
        /// </summary>
        public bool Regenerate { get => _regenerate; set => _regenerate = value; }
        /// <summary>
        /// Gets or sets the regeneration tick time in seconds.
        /// </summary>
        public float RegenerationTick { get => _regenerationTick; set => _regenerationTick = value; }
        /// <summary>
        /// Gets or sets the regeneration rate per tick.
        /// </summary>
        public float RegenerationRate { get => _regenerationRate; set => _regenerationRate = value; }
        /// <summary>
        /// Gets or sets a value indicating whether the resource degenerates.
        /// </summary>
        public bool Degenerate { get => _degenerate; set => _degenerate = value; }
        /// <summary>
        /// Gets or sets the degeneration tick time in seconds.
        /// </summary>
        public float DegenerationTick { get => _degenerationTick; set => _degenerationTick = value; }
        /// <summary>
        /// Gets or sets the degeneration rate per tick.
        /// </summary>
        public float DegenerationRate { get => _degenerationRate; set => _degenerationRate = value; }
        /// <summary>
        /// Gets or sets the low amount threshold.
        /// </summary>
        public float LowAmount { get => _lowAmount; set => _lowAmount = value; }

        /// <summary>
        /// The resource instance managing the resource data.
        /// </summary>
        private Resource _resource = null;
        /// <summary>
        /// WaitForSeconds instance for the cooldown time after increasing the resource amount.
        /// </summary>
        private WaitForSeconds _increaseWaitForSeconds;
        /// <summary>
        /// WaitForSeconds instance for the cooldown time after decreasing the resource amount.
        /// </summary>
        private WaitForSeconds _decreaseWaitForSeconds;
        /// <summary>
        /// WaitForSeconds instance for the regeneration tick interval.
        /// </summary>
        private WaitForSeconds _regenerationWaitForSeconds;
        /// <summary>
        /// WaitForSeconds instance for the degeneration tick interval.
        /// </summary>
        private WaitForSeconds _degenerationWaitForSeconds;
        /// <summary>
        /// Current data of the resource system.
        /// </summary>
        private ResourceSystemData _currentData = new ResourceSystemData();
        /// <summary>
        /// Result of the resource system after an operation.
        /// </summary>
        private ResourceSystemResult _currentResult = new ResourceSystemResult();

        /// <summary>
        /// Event invoked when the resource amount changes.
        /// </summary>
        public UnityEvent<float> AmountChanged = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the resource amount changes. Returns the normalized amount.
        /// </summary>
        public UnityEvent<float> AmountChangedNormalized = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the resource amount is restored.
        /// </summary>
        public UnityEvent<float> AmountRestored = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the resource amount reaches its maximum.
        /// </summary>
        public UnityEvent<float> AmountMaxed = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the resource amount is low.
        /// </summary>
        public UnityEvent<float> AmountLow = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the resource amount is emptied.
        /// </summary>
        public UnityEvent<float> AmountEmptied = new UnityEvent<float>();


        #region Monobehaviour
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
        #endregion


        #region Public Methods
        /// <summary>
        /// Increases the resource amount and returns the result.
        /// The Resource must not be empty. Call RestoreAmount or ResetAmount instead
        /// </summary>
        /// <param name="amountToIncrease">Must be greater than 0</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ResourceSystemResult IncreaseAmountWithResult(float amountToIncrease)
        {
            if (!IncreaseResource(amountToIncrease, true)) { return null; }

            StartCoroutine(IncreaseCooldown());

            return SetResultData();
        }

        /// <summary>
        /// Increases the resource amount.
        /// The Resource must not be empty. Call RestoreAmount or ResetAmount instead
        /// </summary>
        /// <param name="amountToIncrease">Must be greater than 0</param>
        public void IncreaseAmount(float amountToIncrease)
        {
            if (!IncreaseResource(amountToIncrease, true)) { return; }

            StartCoroutine(IncreaseCooldown());
        }

        /// <summary>
        /// Decreases the resource amount with the specified result.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease. Must be greater than 0</param>
        /// <returns>The result of the operation.</returns>
        public ResourceSystemResult DecreaseAmountWithResult(float amountToDecrease)
        {
            if (!DecreaseResource(amountToDecrease, true)) { return null; }

            StartCoroutine(DecreaseCooldown());

            return SetResultData();
        }

        /// <summary>
        /// Decreases the resource amount with the specified result.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease. Must be greater than 0</param>
        public void DecreaseAmount(float amountToDecrease)
        {
            if (!DecreaseResource(amountToDecrease, true)) { return; }

            StartCoroutine(DecreaseCooldown());
        }

        /// <summary>
        /// Restores resource amount only if the resource was empty.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        /// <returns>The result of the operation.</returns>
        public ResourceSystemResult RestoreAmountWithResult(float restorePercentage = 1f)
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
        /// Restores resource amount only if the resource was empty.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        public void RestoreAmount(float restorePercentage = 1f)
        {
            if (!IsEmptyAmount) { return; }
            if (Immutable) { return; }

            if (restorePercentage < 0f || restorePercentage > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(restorePercentage));
            }

            _resource.IncreaseAmount(_resource.MaxAmount * restorePercentage);

            OnResourceChanged();
            OnResourceRestored();
        }

        /// <summary>
        /// Reset Resource to initial amount only if the resource was empty.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        public ResourceSystemResult ResetAmountWithResult()
        {
            if (!IsEmptyAmount) { return null; }
            if (Immutable) { return null; }

            _resource.ResetAmount();

            OnResourceChanged();
            OnResourceRestored();

            return SetResultData();
        }

        /// <summary>
        /// Reset Resource to initial amount only if the resource was empty.
        /// </summary>
        public void ResetAmount()
        {
            if (!IsEmptyAmount) { return; }
            if (Immutable) { return; }

            _resource.ResetAmount();

            OnResourceChanged();
            OnResourceRestored();
        }

        /// <summary>
        /// Clears the resource amount.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        public ResourceSystemResult ClearAmountWithResult()
        {
            if (IsEmptyAmount) { return null; }
            if (Immutable) { return null; }

            _resource.ClearAmount();

            OnResourceEmptied();

            return SetResultData();
        }

        /// <summary>
        /// Clears the resource amount.
        /// </summary>
        public void ClearAmount()
        {
            if (IsEmptyAmount) { return; }
            if (Immutable) { return; }

            _resource.ClearAmount();

            OnResourceEmptied();
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Initializes the resource system with the specified parameters.
        /// </summary>
        private void InitialSetup()
        {
            var resource = new Resource(_name, _description, _initialAmount, _maxAmount);
            _resource = resource;

            _increaseWaitForSeconds = new WaitForSeconds(_increaseCooldownTime);
            _decreaseWaitForSeconds = new WaitForSeconds(_decreaseCooldownTime);
            _regenerationWaitForSeconds = new WaitForSeconds(_regenerationTick);
            _degenerationWaitForSeconds = new WaitForSeconds(_degenerationTick);
        }

        /// <summary>
        /// Increases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the increase was successful, false otherwise.</returns>
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
        
        /// <summary>
        /// Decreases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the decrease was successful, false otherwise.</returns>
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

        /// <summary>
        /// Restores cooldown states on script disable.
        /// </summary>
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

        /// <summary>
        /// Sets the result data based on the current and previous state of the resource system.
        /// </summary>
        /// <returns>The result data containing information about the resource system.</returns>
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
        #endregion


        #region Events
        /// <summary>
        /// Invoked when the resource amount changes. 
        /// </summary>
        private void OnResourceChanged()
        {
            AmountChanged?.Invoke(_resource.Amount);
            AmountChangedNormalized?.Invoke(_resource.AmountNormalized);
        }

        /// <summary>
        /// Invoked when the resource amount is restored.
        /// </summary>
        private void OnResourceRestored()
        {
            AmountRestored?.Invoke(_resource.Amount);
        }

        /// <summary>
        /// Invoked when the resource reaches its maximum amount.
        /// </summary>
        private void OnResourceMaxed()
        {
            AmountMaxed?.Invoke(_resource.Amount);
        }

        /// <summary>
        /// Invoked when the resource amount is low.
        /// </summary>
        private void OnResourceLow()
        {
            AmountLow?.Invoke(_resource.Amount);
        }

        /// <summary>
        /// Invoked when the resource amount is emptied.
        /// </summary>
        private void OnResourceEmptied()
        {
            AmountEmptied?.Invoke(_resource.Amount);
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Handles the regeneration of the resource over time.
        /// </summary>
        private IEnumerator Regeneration()
        {
            if (_regenerationTick == 0f) { yield break; }

            while (true)
            {
                yield return _regenerationWaitForSeconds;

                if (_regenerate)
                {
                    IncreaseResource(_regenerationRate, false);
                }
                else
                {
                    yield return null;
                }
            }
        }

        /// <summary>
        /// Handles the degeneration of the resource over time.
        /// </summary>
        private IEnumerator Degeneration()
        {
            if (_degenerationTick == 0f) { yield break; }

            while (true)
            {
                yield return _degenerationWaitForSeconds;

                if (_degenerate)
                {
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
                else
                {
                    yield return null;
                }
            }
        }

        /// <summary>
        /// Handles the cooldown after increasing the resource amount.
        /// </summary>
        private IEnumerator IncreaseCooldown()
        {
            if (_increaseCooldownTime == 0f) { yield break; }

            IsInIncreaseCooldown = true;
            yield return _increaseWaitForSeconds;
            IsInIncreaseCooldown = false;
            yield break;
        }

        /// <summary>
        /// Handles the cooldown after decreasing the resource amount.
        /// </summary>
        private IEnumerator DecreaseCooldown()
        {
            if (_decreaseCooldownTime == 0f) { yield break; }

            IsInDecreaseCooldown = true;
            yield return _decreaseWaitForSeconds;
            IsInDecreaseCooldown = false;
            yield break;
        }
        #endregion
    }

    /// <summary>
    /// Represents the result data of a resource system operation.
    /// </summary>
    public class ResourceSystemResult
    {
        /// <summary>
        /// Gets or sets the previous state data of the resource system.
        /// </summary>
        public ResourceSystemData PreviousData;
        /// <summary>
        /// Gets or sets the current state data of the resource system.
        /// </summary>
        public ResourceSystemData CurrentData;
    }
}
