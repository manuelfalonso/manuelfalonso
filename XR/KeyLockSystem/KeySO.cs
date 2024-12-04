using UnityEngine;

namespace SombraStudios.Shared.XR.KeyLockSystem
{
    /// <summary>
    /// An asset that represents a key. Used to check if an object can perform some action
    /// (<see cref="SocketInteractor.XRLockSocketInteractor"/> and <see cref="Keychain"/>)
    /// </summary>
    [CreateAssetMenuAttribute(menuName = "XR/Key Lock System/Key")]
    public class KeySO : ScriptableObject { }
}
