using SombraStudios.Shared.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Gameplay.Behaviours.Charge
{
    /// <summary>
    /// Manages a charging behavior with customizable parameters such as time-based charging,
    /// maximum charges, and automatic discharge. Provides events for tracking charging progress
    /// and allows for starting, stopping, and using charges.
    /// </summary>
    public class ChargeBehaviour : MonoBehaviour, IBehaviour
    {
        [Header("Data")]
        /// <summary>
        /// Holds the data configuration for the charging behavior.
        /// </summary>
        [Tooltip("Holds the data configuration for the charging behavior.")]
        [SerializeField] private ChargeBehaviourSO _data;

        [Header("Debug")]
        /// <summary>
        /// Indicates whether the charging behavior is active.
        /// </summary>
        [Tooltip("Indicates whether the charging behavior is active.")]
        [SerializeField] private bool _isEnabled = true;
        /// <summary>
        /// Indicates whether to show debug logs.
        /// </summary>
        [Tooltip("Indicates whether to show debug logs.")]
        [SerializeField] private bool _showLogs = false;
        /// <summary>
        /// Current number of charges.
        /// </summary>
        [Tooltip("Current number of charges.")]
        [SerializeField, ReadOnly] private int _charges = 0;
        /// <summary>
        /// Total accumulated charge value.
        /// </summary>
        [Tooltip("Total accumulated charge value.")]
        [SerializeField, ReadOnly] private float _totalCharge = 0f;
        /// <summary>
        /// Total accumulated discharge value.
        /// </summary>
        [Tooltip("Total accumulated discharge value.")]
        [SerializeField, ReadOnly] private float _totalDischarge = 0f;
        /// <summary>
        /// Indicates whether the charging process is currently active.
        /// </summary>
        [Tooltip("Indicates whether the charging process is currently active.")]
        [SerializeField, ReadOnly] private bool _isCharging = false;
        /// <summary>
        /// Indicates whether the maximum number of charges has been reached.
        /// </summary>
        [Tooltip("Indicates whether the maximum number of charges has been reached.")]
#pragma warning disable IDE0052 // Quitar miembros privados no le�dos
        [SerializeField, ReadOnly] private bool _isMaxCharged = false;
#pragma warning restore IDE0052 // Quitar miembros privados no le�dos


        /// <summary>
        /// Gets or sets the charging behavior data.
        /// </summary>
        public ChargeBehaviourSO Data { get => _data; set => _data = value; }
        /// <summary>
        /// Gets or sets whether the charging behavior is active.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
        /// <summary>
        /// Gets the current number of charges.
        /// </summary>
        public int Charges
        {
            get
            {
                return _charges;
            }
            private set
            {
                // Validate minimun amount
                if (value <= 0)
                {
                    _charges = 0;
                    _totalCharge = 0f;
                    _totalDischarge = 0f;
                }
                else
                {
                    _totalCharge = value;
                    _charges = value;
                }

                // Max Charges reached?
                if (IsMaxCharged)
                {
                    _isMaxCharged = true;
                    ChargingEnded?.Invoke();
                }
                else
                {
                    _isMaxCharged = false;
                }

                ChargeReached?.Invoke(_charges);
                
                if (_showLogs)
                    Utility.Loggers.Logger.Log($"Charges: {_charges}", this);
            }
        }
        /// <summary>
        /// Gets the total accumulated charge value.
        /// </summary>
        public float TotalCharge { get => _totalCharge; private set => _totalCharge = value; }
        /// <summary>
        /// Gets whether the charging process is currently active.
        /// </summary>
        public bool IsCharging { get => _isCharging; private set => _isCharging = value; }

        /// <summary>
        /// Gets whether the maximum number of charges has been reached.
        /// </summary>
        public bool IsMaxCharged => _data.MaxCharges > 0 && Charges == _data.MaxCharges;

        // Events
        /// <summary>
        /// Event invoked when charging starts.
        /// </summary>
        [Tooltip("Event invoked when charging starts.")]
        public UnityEvent ChargingStarted = new UnityEvent();
        /// <summary>
        /// Event invoked when charging stops.
        /// </summary>
        [Tooltip("Event invoked when charging stops.")]
        public UnityEvent ChargingStoped = new UnityEvent();
        /// <summary>
        /// Event invoked when the maximum charges are reached.
        /// </summary>
        [Tooltip("Event invoked when the maximum charges are reached.")]
        public UnityEvent ChargingEnded = new UnityEvent();

        /// <summary>
        /// Event invoked on each charge.
        /// </summary>
        [Tooltip("Invoked on each charge.")]
        public UnityEvent<int> ChargeReached = new UnityEvent<int>();
        /// <summary>
        /// Event invoked on each discharge.
        /// </summary>
        [Tooltip("Invoked on each discharge.")]
        public UnityEvent<int> DischargeReached = new UnityEvent<int>();

        /// <summary>
        /// Toggles the behaviour on or off.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        // Charge
        /// <summary>
        /// Gets the next charge value.
        /// </summary>
        private float _nextCharge => _charges + 1;
        /// <summary>
        /// Gets the time in seconds for the next charge to accumulate.
        /// </summary>
        private float _nextChargeTime => _data.TimePerCharge * _nextCharge;


        /// <summary>
        /// Called every frame to update the charging behavior.
        /// </summary>
        private void Update()
        {
            ExecuteBehaviour();
        }



        /// <summary>
        /// Starts the charging process.
        /// </summary>
        /// <returns>True if charging started successfully, false otherwise.</returns>
        public bool StartCharging()
        {
            if (!_isEnabled) { return false; }
            if (_isCharging)
            {
                // Its already in charging state
                return false;
            }
            else
            {
                // Start the charge
                _isCharging = true;
                ChargingStarted?.Invoke();

                if (_showLogs)
                    Utility.Loggers.Logger.Log($"StartCharging", this);

                return true;
            }
        }


        /// <summary>
        /// Stops the charging process.
        /// </summary>
        /// <returns>True if charging stopped successfully, false otherwise.</returns>
        public bool StopCharging()
        {
            if (!_isEnabled) { return false; }
            if (!_isCharging)
            {
                // Its already stopped
                return false;
            }
            else
            {
                // Stop the charge
                _isCharging = false;
                ChargingStoped?.Invoke();

                if (_showLogs)
                    Utility.Loggers.Logger.Log($"StopCharging: {_totalCharge}", this);

                if (!_data.MantainThresholdOnStop) { _totalCharge = Charges; }

                if (_data.AutoUseOnStop) { UseCharges(); }

                return true;
            }
        }


        /// <summary>
        /// Resets charges to the initial value.
        /// </summary>
        /// <returns>A result indicating the success of the operation along with charge information.</returns>
        public ChargeBehaviourResult UseCharges()
        {
            ChargeBehaviourResult result = new ChargeBehaviourResult();
            if (!_isEnabled) { return result; }

            result.Charges = _charges;
            result.TotalCharge = _totalCharge;

            if (_charges > 0)
            {
                // Reset charges
                if (_showLogs)
                    Utility.Loggers.Logger.Log($"UseCharges: {_charges}", this);

                _charges = 0;
                _totalCharge = 0f;
                _totalDischarge = 0f;
                _isCharging = false;

                result.Success = true;
            }
            else
            {
                // There weren't charges reached
                result.Success = false;
            }

            return result;
        }


        /// <summary>
        /// Handles the charging behavior based on the configured parameters.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (!_isEnabled) { return; }

            // Check if the data required is assinged, if not stop running the script
            if (_data == null)
            {
                Debug.LogError($"Behaviour data missing!", this);
                enabled = false;
                _isEnabled = false;
                return;
            }

            // Gaining Charge
            if (_isCharging)
            {
                // Calcute the Total Charge value
                if (_data.IsTimeScaleDependant)
                {
                    _totalCharge += Time.deltaTime;
                }
                else
                {
                    _totalCharge += Time.unscaledDeltaTime;
                }

                // Check if a new Charge was reached
                if (_totalCharge >= _nextChargeTime)
                {
                    Charges++;
                }

                if (_data.MaxCharges != 0 && _charges >= _data.MaxCharges) { StopCharging(); }
            }
            // Losing Charge
            else if (_data.LoseChargesAfterTime)
            {
                if (Charges <= 0) { return; }

                // Calcute the Total Discharge value
                if (_data.IsTimeScaleDependant)
                {
                    _totalDischarge += Time.deltaTime;
                }
                else
                {
                    _totalDischarge += Time.unscaledDeltaTime;
                }

                // Check if a new Discharge was reached
                if (_totalDischarge >= _data.TimeToLoseCharges)
                {
                    ApplyDischarge();
                }
            }
        }

        /// <summary>
        /// Applies the discharge based on the configured parameters.
        /// </summary>
        private void ApplyDischarge()
        {
            _totalDischarge -= _data.TimeToLoseCharges;
            // Set new charge
            Charges -= _data.ChargesLoseAfterTime;
            DischargeReached?.Invoke(Charges);
        }
    }

    /// <summary>
    /// Result structure indicating the success of a charge behavior operation along with charge information.
    /// </summary>
    public struct ChargeBehaviourResult
    {
        public bool Success;
        public int Charges;
        public float TotalCharge;
    }
}
