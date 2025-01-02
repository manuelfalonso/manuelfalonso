#if UNITY_XR_INTERACTION_TOOLKIT
using SombraStudios.Shared.XR.KeyLockSystem;
using UnityEngine;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#else
using UnityEngine.XR.Interaction.Toolkit;
#endif

namespace SombraStudios.Shared.XR.SocketInteractor
{
    /// <summary>
    /// Grid Socket interactor that only selects and hovers interactables with a <see cref="Keychain"/> component containing specific keys.
    /// </summary>
    public class XRLockGridSocketInteractor : XRGridSocketInteractor
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
            if (!base.CanHover(interactable))
                return false;

            var keyChain = interactable.transform.GetComponent<IKeychain>();
            return _lock.CanUnlock(keyChain);
        }

        /// <inheritdoc />
        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            if (!base.CanSelect(interactable))
                return false;

            var keyChain = interactable.transform.GetComponent<IKeychain>();
            return _lock.CanUnlock(keyChain);
        }
    }
}
#endif