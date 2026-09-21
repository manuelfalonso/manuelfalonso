using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing common Unity MonoBehaviour lifecycle messages.
    /// </summary>
    [Flags]
    public enum UnityMonoBehaviourMessages
    {
        None = 0,
        /// <summary>
        /// The Awake message.
        /// </summary>
        Awake = 1 << 0,
        /// <summary>
        /// The Start message.
        /// </summary>
        Start = 1 << 1,
        /// <summary>
        /// The OnEnable message.
        /// </summary>
        OnEnable = 1 << 2,
        /// <summary>
        /// The OnDisable message.
        /// </summary>
        OnDisable = 1 << 3,
        /// <summary>
        /// The OnDestroy message.
        /// </summary>
        OnDestroy = 1 << 4
    }
}
