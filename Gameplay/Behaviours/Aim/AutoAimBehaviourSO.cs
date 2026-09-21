using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.Aim
{
    /// <summary>
    /// Settings for the auto-aim behaviour.
    /// </summary>
    [CreateAssetMenu(fileName = "AutoAimSettings", menuName = "Sombra Studios/Behaviours/AutoAimSettings")]
    public class AutoAimBehaviourSO : ScriptableObject
    {
        [Header("Aim")]
        [Tooltip("Speed at which the aim moves.")]
        public float AimSpeed = 5f;

        [Header("Prediction")]
        [Tooltip("If true, the aim will be predicted based on the target's velocity of the Rigidbody.")]
        public bool UsePrediction = true;

        [Tooltip("Factor by which the target's velocity is multiplied for prediction.")]
        public float PredictionVelocityFactor = 1f;

        [Header("Deadzone")]
        [Tooltip("Distance within which the aim will not adjust.")]
        public float DeadzoneDistance = .15f;
        [Tooltip("Reaction delay to get out of deadzone.")]
        public float OutOfDeadzoneDelay = 0.25f;

        [Header("Offset")]
        [Tooltip("Offset applied to the aim position.")]
        public Vector3 TargetOffset = Vector3.zero;

        [Header("Constraints")]
        [Tooltip("If true, constraints will be applied to the aim.")]
        public bool UseConstraints = false;

        [Tooltip("If true, the aim will be constrained on the X axis.")]
        public bool XConstraint = false;

        [Tooltip("If true, the aim will be constrained on the Y axis.")]
        public bool YConstraint = false;

        [Tooltip("If true, the aim will be constrained on the Z axis.")]
        public bool ZConstraint = false;

        [Header("Noise")]
        [Tooltip("If true, procedural noise will be applied to the aim.")]
        public bool UseNoise = false;

        [Tooltip("Speed at which the noise evolves over time.")]
        public float NoiseFrequency = 1f;

        [Tooltip("Maximum angular offset, in degrees, applied on each axis.")]
        public Vector3 NoiseStrength = new(0.2f, 0.2f, 0f);
    }
}
