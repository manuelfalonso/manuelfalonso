#if UNITY_XR_INTERACTION_TOOLKIT
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#endif

namespace SombraStudios.Shared.XR
{
    /// <summary>
    /// Trigger haptics on interactable object using Haptic configuration on demand or on events
    /// </summary>
    public class HapticInteractable : MonoBehaviour
    {
        [Header("References")]
        /// <summary>
        /// Interactable object that will trigger haptic feedback on events.
        /// </summary>
        [SerializeField] private XRBaseInteractable _interactable;
        /// <summary>
        /// Haptic feedback that will be triggered on demand.
        /// </summary>
        [SerializeField] private Haptic _hapticOnDemand;

        [Header("Events")]
        /// <summary>
        /// Haptic feedback on interactable activated event.
        /// </summary>
        [SerializeField] private Haptic _hapticOnActivated;
        /// <summary>
        /// Haptic feedback on interactable deactivated event.
        /// </summary>
        [SerializeField] private Haptic _hapticOnDeactivated;
        /// <summary>
        /// Haptic feedback on interactable hover enter event.
        /// </summary>
        [SerializeField] private Haptic _hapticHoverEntered;
        /// <summary>
        /// Haptic feedback on interactable hover exit event.
        /// </summary>
        [SerializeField] private Haptic _hapticHoverExited;
        /// <summary>
        /// Haptic feedback on interactable selected enter event.
        /// </summary>
        [SerializeField] private Haptic _hapticSelectEntered;
        /// <summary>
        /// Haptic feedback on interactable selected exited event.
        /// </summary>
        [SerializeField] private Haptic _hapticSelectExited;


        private void OnEnable()
        {
            AddHapticListeners();
        }

        private void OnDisable()
        {
            RemoveHapticListeners();
        }


        /// <summary>
        /// Tries to trigger on demand haptic feedback.
        /// </summary>
        /// <returns>True if the haptic feedback was successfully triggered; otherwise, false.</returns>
        public bool TryTriggerHaptic()
        {
            if (CanTriggerHaptic(out XRBaseInputInteractor controller))
            {
                _hapticOnDemand.TriggerHaptic(controller);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to trigger on demand haptic feedback with specified intensity and duration.
        /// </summary>
        /// <param name="intensity">The intensity of the haptic feedback.</param>
        /// <param name="duration">The duration of the haptic feedback.</param>
        /// <returns>True if the haptic feedback was successfully triggered; otherwise, false.</returns>
        public bool TryTriggerHaptic(float intensity, float duration)
        {
            _hapticOnDemand.Intensity = intensity;
            _hapticOnDemand.Duration = duration;

            if (CanTriggerHaptic(out XRBaseInputInteractor controller))
            {
                _hapticOnDemand.TriggerHaptic(controller);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if haptic feedback can be triggered and retrieves the controller interactor.
        /// </summary>
        /// <param name="controller">The controller interactor if haptic feedback can be triggered; otherwise, null.</param>
        /// <returns>True if haptic feedback can be triggered; otherwise, false.</returns>
        private bool CanTriggerHaptic(out XRBaseInputInteractor controller)
        {
            if (_interactable.firstInteractorSelecting is XRBaseInputInteractor hand)
            {
                controller = hand;
                return true;
            }
            else
            {
                controller = null;
                return false;
            }
        }

        /// <summary>
        /// Adds haptic listeners to the interactable object events.
        /// </summary>
        private void AddHapticListeners()
        {
            if (_interactable == null)
            {
                Debug.LogError("Interactable is not assigned");
                return;
            }

            RemoveHapticListeners();
            _interactable.activated.AddListener(_hapticOnActivated.TriggerHaptic);
            _interactable.deactivated.AddListener(_hapticOnDeactivated.TriggerHaptic);
            _interactable.hoverEntered.AddListener(_hapticHoverEntered.TriggerHaptic);
            _interactable.hoverExited.AddListener(_hapticHoverExited.TriggerHaptic);
            _interactable.selectEntered.AddListener(_hapticSelectEntered.TriggerHaptic);
            _interactable.selectExited.AddListener(_hapticSelectExited.TriggerHaptic);
        }

        /// <summary>
        /// Removes haptic listeners from the interactable object events.
        /// </summary>
        private void RemoveHapticListeners()
        {
            if (_interactable == null)
            {
                Debug.LogError("Interactable is not assigned");
                return;
            }
            _interactable.activated.RemoveListener(_hapticOnActivated.TriggerHaptic);
            _interactable.deactivated.RemoveListener(_hapticOnDeactivated.TriggerHaptic);
            _interactable.hoverEntered.RemoveListener(_hapticHoverEntered.TriggerHaptic);
            _interactable.hoverExited.RemoveListener(_hapticHoverExited.TriggerHaptic);
            _interactable.selectEntered.RemoveListener(_hapticSelectEntered.TriggerHaptic);
            _interactable.selectExited.RemoveListener(_hapticSelectExited.TriggerHaptic);
        }
    }

    /// <summary>
    /// Configuration for haptic feedback.
    /// </summary>
    [System.Serializable]
    public class Haptic
    {
        [Range(0, 1)]
        public float Intensity;
        public float Duration;


        /// <summary>
        /// Triggers haptic feedback based on the provided interaction event arguments.
        /// </summary>
        /// <param name="baseInteractionEventArgs">The interaction event arguments.</param>
        public void TriggerHaptic(BaseInteractionEventArgs baseInteractionEventArgs)
        {
            if (baseInteractionEventArgs.interactorObject is XRBaseInputInteractor xRBaseControllerInteractor)
            {
                TriggerHaptic(xRBaseControllerInteractor);
            }
        }

        /// <summary>
        /// Triggers haptic feedback on the provided controller interactor.
        /// </summary>
        /// <param name="xRBaseControllerInteractor">The controller interactor to trigger haptic feedback on.</param>
        public void TriggerHaptic(XRBaseInputInteractor xRBaseControllerInteractor)
        {
            if (Intensity > 0f && Duration > 0f)
            {
                xRBaseControllerInteractor.SendHapticImpulse(Intensity, Duration);
            }
        }
    }
}
#endif