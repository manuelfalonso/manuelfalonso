using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Resource system for managing a float resource.
    /// Supports:
    /// - Increasing and decreasing resource amount.
    /// - Restoring resource to a defined amount.
    /// - Locking amount with cooldowns during increase or decrease.
    /// - Immutable resource behavior.
    /// - Events:
    ///   AmountChanged, AmountRestored, AmountMaxed, AmountEmptied.
    /// 
    /// Can be used for any type of resource such as life, mana, wood, or water.
    /// </summary>
    [Serializable]
    public class FloatResourceSystem : ResourceSystem<float>
    {
        /// <summary>
        /// Gets the cooldown duration for increasing the resource.
        /// </summary>
        public float IncreaseCooldownDuration { get; protected set; }
        
        /// <summary>
        /// Gets the cooldown duration for decreasing the resource.
        /// </summary>
        public float DecreaseCooldownDuration { get; protected set; }

        /// <summary>
        /// Gets the current amount of the resource, or 0 if empty.
        /// </summary>
        public float Amount => IsEmptyAmount ? 0f : _resource.Amount;

        /// <summary>
        /// Gets the maximum amount the resource can hold.
        /// </summary>
        public float MaxAmount => _resource.MaxAmount;

        /// <summary>
        /// Gets the percentage of the current resource amount compared to the maximum amount.
        /// </summary>
        public float ResourcePercent => IsEmptyAmount ? 0f : (float)_resource.Amount / _resource.MaxAmount;

        /// <summary>
        /// Checks if the resource is at its maximum amount.
        /// </summary>
        public bool IsMaxAmount => _resource.Amount >= _resource.MaxAmount;

        /// <summary>
        /// Checks if the resource is empty.
        /// </summary>
        public bool IsEmptyAmount => _resource?.Amount <= 0;

        /// <summary>
        /// Indicates whether the resource is immutable.
        /// </summary>
        public bool IsImmutable { get; set; }

        /// <summary>
        /// Checks if the resource is in an increase cooldown.
        /// </summary>
        public bool IsInIncreaseCooldown { get; protected set; } = false;

        /// <summary>
        /// Checks if the resource is in a decrease cooldown.
        /// </summary>
        public bool IsInDecreaseCooldown { get; protected set; } = false;

        /// <summary>
        /// Checks if the resource system has been initialized.
        /// </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary>
        /// The resource instance managing the resource data.
        /// </summary>
        [SerializeField]
        private FloatResource _resource;

        /// <summary>
        /// Holds the current data of the resource system.
        /// </summary>
        [NonSerialized]
        private readonly ResourceSystemData _currentData = new();

        /// <summary>
        /// Event invoked when the resource amount changes.
        /// </summary>
        public UnityEvent<ResourceSystemData> AmountChanged = new();

        /// <summary>
        /// Event invoked when the resource is restored.
        /// </summary>
        public UnityEvent<ResourceSystemData> AmountRestored = new();

        /// <summary>
        /// Event invoked when the resource reaches its maximum amount.
        /// </summary>
        public UnityEvent<ResourceSystemData> AmountMaxed = new();

        /// <summary>
        /// Event invoked when the resource is emptied.
        /// </summary>
        public UnityEvent<ResourceSystemData> AmountEmptied = new();


        #region Unity Messages
        protected virtual void OnDisable()
        {
            RestoreCooldowns();
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Calculates the resource amount based on a percentage of the maximum amount.
        /// </summary>
        /// <param name="percentage">Percentage of the maximum amount.</param>
        /// <returns>The calculated resource amount.</returns>
        public float GetResourceAmountBasedOnPercentage(float percentage) => MaxAmount * percentage;

        /// <summary>
        /// Initializes the resource system with specified parameters.
        /// </summary>
        /// <param name="name">Name of the resource.</param>
        /// <param name="description">Description of the resource.</param>
        /// <param name="minAmount">Minimum allowed resource amount.</param>
        /// <param name="maxAmount">Maximum allowed resource amount.</param>
        /// <param name="initialAmount">Initial resource amount.</param>
        /// <param name="increaseCooldownDuration">Cooldown duration for increases.</param>
        /// <param name="decreaseCooldownDuration">Cooldown duration for decreases.</param>
        public void Setup(
            string name,
            string description,
            float minAmount,
            float maxAmount,
            float initialAmount,
            float increaseCooldownDuration,
            float decreaseCooldownDuration)
        {
            try
            {
                var resource = new FloatResource(name, description, minAmount, maxAmount, initialAmount);
                _resource = resource;

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
            IncreaseCooldownDuration = increaseCooldownDuration;
            DecreaseCooldownDuration = decreaseCooldownDuration;
        }

        /// <summary>
        /// Initializes the resource system using a FloatResourceSystemData object.
        /// </summary>
        /// <param name="data">The data used to configure the resource system.</param>
        public void Setup(FloatResourceSystemData data)
        {
            Setup(data.Name, data.Description, data.MinAmount, data.MaxAmount, data.InitialAmount, 
                data.IncreaseCooldownDuration, data.DecreaseCooldownDuration);
        }
        #endregion


        #region Protected Methods
        protected override bool TryIncreaseAmount(float amountToIncrease, 
            out (ResourceSystemData previous, ResourceSystemData current) result,
            bool considerCooldonws = true)
        {
            var previousData = _currentData.Clone();

            if (CanIncreaseAmount(amountToIncrease, considerCooldonws))
            {
                DoIncreaseAmount(amountToIncrease);
                
                IsInIncreaseCooldown = true;
                Invoke(nameof(ResetIncreaseCooldown), IncreaseCooldownDuration);
                
                result = (previousData, _currentData);
                return true;
            }

            result = (previousData, _currentData);
            return false;
        }

        protected override bool TryDecreaseAmount(float amountToDecrease, 
            out (ResourceSystemData previous, ResourceSystemData current) result,
            bool considerCooldonws = true)
        {
            var previousData = _currentData.Clone();

            if (CanDecreaseAmount(amountToDecrease, considerCooldonws))
            {
                DoDecreaseAmount(amountToDecrease);

                IsInDecreaseCooldown = true;
                Invoke(nameof(ResetDecreaseCooldown), DecreaseCooldownDuration);

                result = (previousData, _currentData);
                return true;
            }

            result = (previousData, _currentData);
            return false;
        }

        protected override bool TryRestoreAmount(out (ResourceSystemData previous, ResourceSystemData current) result, 
            float restorePercentage = 1f)
        {
            var previousData = _currentData.Clone();

            try
            {
                if (CanRestoreAmount(restorePercentage))
                {
                    DoRestoreAmount(restorePercentage);

                    result = (previousData, _currentData);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }

            result = (previousData, _currentData);
            return false;
        }

        protected override bool TryResetAmount(out (ResourceSystemData previous, ResourceSystemData current) result, 
            bool validateEmpty = false)
        {
            var previousData = _currentData.Clone();

            if (CanResetAmount(validateEmpty))
            {
                DoResetAmount();

                result = (previousData, _currentData);
                return true;
            }

            result = (previousData, _currentData);
            return false;
        }

        protected override bool TryClearAmount(out (ResourceSystemData previous, ResourceSystemData current) result)
        {
            var previousData = _currentData.Clone();

            if (CanClearAmount())
            {
                DoClearAmount();

                result = (previousData, _currentData);
                return true;
            }

            result = (previousData, _currentData);
            return false;
        }

        protected override void StartRegeneration(RegenerationData<float> data)
        {
            StartCoroutine(Regeneration(data));
        }

        protected override void StartDegeneration(RegenerationData<float> data)
        {
            StartCoroutine(Degeneration(data));
        }

        protected override bool CanIncreaseAmount(float amountToIncrease, bool considerCooldowns)
        {
            if (amountToIncrease <= 0) { return false; }
            if (IsImmutable) { return false; }
            if (considerCooldowns)
            {
                if (IsInIncreaseCooldown) { return false; }
            }

            return true;
        }

        protected override void DoIncreaseAmount(float amountToIncrease)
        {
            _resource.IncreaseResource(amountToIncrease);

            OnResourceChanged();
            if (IsMaxAmount)
            {
                OnResourceMaxed();
            }
        }

        protected override bool CanDecreaseAmount(float amountToDecrease, bool considerCooldowns)
        {
            if (amountToDecrease <= 0) { return false; }
            if (IsEmptyAmount) { return false; }
            if (IsImmutable) { return false; }
            if (considerCooldowns)
            {
                if (IsInDecreaseCooldown) { return false; }
            }

            return true;
        }

        protected override void DoDecreaseAmount(float amountToDecrease)
        {
            _resource.DecreaseResource(amountToDecrease);

            OnResourceChanged();
            if (IsEmptyAmount)
            {
                OnResourceEmptied();
            }
        }

        protected override bool CanRestoreAmount(float restorePercentage)
        {
            if (!IsEmptyAmount) { return false; }
            if (IsImmutable) { return false; }

            if (restorePercentage < 0f || restorePercentage > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(restorePercentage));
            }

            return true;
        }

        protected override void DoRestoreAmount(float restorePercentage)
        {
            int value = Mathf.RoundToInt((_resource.MaxAmount - _resource.MinAmount) * restorePercentage);
            _resource.IncreaseResource(value);

            OnResourceChanged();
            OnResourceRestored();
        }

        protected override bool CanResetAmount(bool validateEmpty)
        {
            if (validateEmpty)
            {
                if (!IsEmptyAmount) { return false; }
            }
            if (IsImmutable) { return false; }

            return true;
        }

        protected override void DoResetAmount()
        {
            _resource.ResetResource();

            OnResourceChanged();
            OnResourceRestored();
        }

        protected override bool CanClearAmount()
        {
            if (IsEmptyAmount) { return false; }
            if (IsImmutable) { return false; }

            return true;
        }

        protected override void DoClearAmount()
        {
            _resource.ClearResource();

            OnResourceChanged();
            OnResourceEmptied();
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Resets all cooldowns to their default state.
        /// </summary>
        private void RestoreCooldowns()
        {
            IsInDecreaseCooldown = IsInIncreaseCooldown = false;
        }

        /// <summary>
        /// Updates the current data of the resource system.
        /// </summary>
        private void UpdateCurrentData()
        {
            _currentData.Amount = Amount;
            _currentData.MaxAmount = MaxAmount;
            _currentData.Percent = ResourcePercent;
            _currentData.IsMaxAmount = IsMaxAmount;
            _currentData.IsEmptyAmount = IsEmptyAmount;
            _currentData.Immutable = IsImmutable;
            _currentData.IsInCooldown = IsInDecreaseCooldown || IsInIncreaseCooldown;
        }
        #endregion


        #region Events
        /// <summary>
        /// Handles the event when the resource amount changes.
        /// </summary>
        private void OnResourceChanged()
        {
            UpdateCurrentData();
            AmountChanged?.Invoke(_currentData);
        }

        /// <summary>
        /// Handles the event when the resource is restored.
        /// </summary>
        private void OnResourceRestored()
        {
            AmountRestored?.Invoke(_currentData);
        }

        /// <summary>
        /// Handles the event when the resource reaches its maximum amount.
        /// </summary>
        private void OnResourceMaxed()
        {
            AmountMaxed?.Invoke(_currentData);
        }

        /// <summary>
        /// Handles the event when the resource is emptied.
        /// </summary>
        private void OnResourceEmptied()
        {
            AmountEmptied?.Invoke(_currentData);
        }
        #endregion


        #region Coroutines
        /// <summary>
        /// Coroutine for regenerating the resource.
        /// </summary>
        /// <param name="data">Regeneration data.</param>
        /// <returns>Enumerator for coroutine execution.</returns>
        private IEnumerator Regeneration(RegenerationData<float> data)
        {
            if (data.RegenerationTick == 0f) { yield break; }

            WaitForSeconds waitForSeconds = new(data.RegenerationTick);
            float time = 0f;

            while (time < data.RegenerationTime)
            {
                yield return waitForSeconds;
                TryIncreaseAmount(data.RegenerationAmountRate, out _, false);
                time += data.RegenerationTick;
            }
        }

        /// <summary>
        /// Coroutine for degenerating the resource.
        /// </summary>
        /// <param name="data">Degeneration data.</param>
        /// <returns>Enumerator for coroutine execution.</returns>
        private IEnumerator Degeneration(RegenerationData<float> data)
        {
            if (data.RegenerationTick == 0f) { yield break; }

            WaitForSeconds waitForSeconds = new(data.RegenerationTick);
            float time = 0f;

            while (time < data.RegenerationTime)
            {
                yield return waitForSeconds;
                TryDecreaseAmount(data.RegenerationAmountRate, out _, false);
                time += data.RegenerationTick;
            }
        }

        private void ResetIncreaseCooldown()
        {
            IsInIncreaseCooldown = false;
        }

        private void ResetDecreaseCooldown()
        {
            IsInDecreaseCooldown = false;
        }
        #endregion
    }
}
