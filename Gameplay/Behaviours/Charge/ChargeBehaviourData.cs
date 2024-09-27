using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.Charge
{
    /// <summary>
    /// Holds data for configuring charging behavior, including parameters for time-based charging,
    /// maximum charges, automatic discharge, and time scale dependency. Provides validation during
    /// editor time to ensure data consistency.
    /// </summary>
    [CreateAssetMenu(fileName = "Charge Behaviour", menuName = "Sombra Studios/Behaviours/Charge Behaviour", order = 0)]
    public class ChargeBehaviourData : ScriptableObject
    {
        [Header("Main")]
        /// <summary>
        /// Time in seconds required for each charge to accumulate.
        /// </summary>
        [Tooltip("Time in seconds required for each charge to accumulate.")]
        public float TimePerCharge = 1f;

        /// <summary>
        /// Maximum number of charges. Set to 0 for infinite charges.
        /// </summary>
        [Tooltip("Maximum number of charges. Set to 0 for infinite charges.")]
        public int MaxCharges = 0;

        [Header("Configuration")]
        /// <summary>
        /// Automatically use charges when charging is stopped.
        /// </summary>
        [Tooltip("Automatically use charges when charging is stopped.")]
        public bool AutoUseOnStop = false;
        /// <summary>
        /// Maintain the charge threshold when charging is stopped.
        /// </summary>
        [Tooltip("Maintain the charge threshold when charging is stopped.")]
        public bool MantainThresholdOnStop = false;
        /// <summary>
        /// Indicates whether charge accumulation is dependent on Time.timeScale.
        /// Changing Time.timeScale will affect the charge time.
        /// </summary>
        [Tooltip("Changing Time.timeScale will also affect the charge time.")]
        public bool IsTimeScaleDependant = true;

        [Header("Lost of Charges")]
        /// <summary>
        /// Indicates whether charges are lost over time.
        /// </summary>
        [Tooltip("Indicates whether charges are lost over time.")]
        public bool LoseChargesAfterTime = false;
        /// <summary>
        /// Time interval in seconds for losing a charge when LoseChargesAfterTime is true.
        /// </summary>
        [Tooltip("Time interval in seconds for losing a charge when LoseChargesAfterTime is true.")]
        public float TimeToLoseCharges = 0f;
        /// <summary>
        /// Number of charges lost after each TimeToLoseCharges interval.
        /// </summary>
        [Tooltip("Number of charges lost after each TimeToLoseCharges interval.")]
        public int ChargesLoseAfterTime = 1;


        /// <summary>
        /// Validates the data during editor time to ensure consistency.
        /// </summary>
        private void OnValidate()
        {
            ValidateData();
        }


        /// <summary>
        /// Validates the data, ensuring that all values are within acceptable ranges.
        /// </summary>
        private void ValidateData()
        {
            // Must be positive
            if (TimePerCharge <= 0f) { TimePerCharge = 1f; }
            // Must be positve or 0 for infinite charges
            if (MaxCharges < 0) { MaxCharges = 0; }

            // Must be positive
            if (TimeToLoseCharges <= 0f) { TimeToLoseCharges = 1f; }
            // Must be positive
            if (ChargesLoseAfterTime <= 0) { MaxCharges = 1; }
        }
    }
}
