#if UNITY_XR_INTERACTION_TOOLKIT
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Socket interactor that only selects and hovers interactables with a keychain component containing specific keys.
    /// </summary>
    public class XRLockSocketInteractor : XRSocketInteractor
    {
        [Space]        
        [Tooltip("The required keys to interact with this socket.")]
        [SerializeField] private Lock _lock;

        /// <summary>
        /// The required keys to interact with this socket.
        /// </summary>
        public Lock KeychainLock
        {
            get => _lock;
            set => _lock = value;
        }


        /// <inheritdoc />
        public override bool CanHover(IXRHoverInteractable interactable)
        {
            if (!base.CanHover(interactable)) { return false; }                

            var keyChain = interactable.transform.GetComponent<IKeychain>();
            return _lock.CanUnlock(keyChain);
        }

        /// <inheritdoc />
        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            if (!base.CanSelect(interactable)) { return false; }                

            var keyChain = interactable.transform.GetComponent<IKeychain>();
            return _lock.CanUnlock(keyChain);
        }
    }
}
#endif