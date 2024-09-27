#if UNITY_XR_INTERACTION_TOOLKIT
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SombraStudios.Shared.XR
{
    /// <summary>
    /// Initializes an <see cref="XRSocketInteractor"/> attach point to match the initial scene position of the object it is containing.
    /// </summary>
    public class AutoSocketAttach : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Socket Interactor that controls this socket attach point.")]
        XRSocketInteractor _controllingInteractor;

        void Start()
        {
            // If there is an existing interactable, we match its position so the object does not move
            if (_controllingInteractor == null) { _controllingInteractor = GetComponentInParent<XRSocketInteractor>(); }                

            if (_controllingInteractor == null)
            {
                Debug.LogWarning("Script is not associated with an XRSocketInteractor and will have no effect.", this);
                return;
            }

            if (_controllingInteractor.startingSelectedInteractable == null)
            {
                Debug.Log("AutoSocketAttach does not have a starting selected interactable to match its position.", this);
                return;
            }

            var targetTransform = _controllingInteractor.startingSelectedInteractable.GetAttachTransform(_controllingInteractor);
            transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);
        }
    }
}
#endif