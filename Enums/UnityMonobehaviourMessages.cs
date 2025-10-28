using System;

namespace SombraStudios.Shared.Enums
{
    [Flags, Serializable]
    /// <summary>
    /// Enum representing common Unity MonoBehaviour lifecycle messages.
    /// </summary>
    public enum UnityMonobehaviourMessages
    {
        None = 0,
        Awake = 1 << 0,
        Start = 1 << 1,
        OnEnable = 1 << 2,
        OnDisable = 1 << 3,
        OnDestroy = 1 << 4
    }
}
