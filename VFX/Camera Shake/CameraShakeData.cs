using UnityEngine;

namespace SombraStudios.VFX
{
    /// <summary>
    /// Store Shake Data to be used with DoTween DOShakePosition method
    /// </summary>
    [CreateAssetMenu(fileName = "Shake Data", menuName = "ScriptableObjects/Camera/Shake", order = 51)]
    public class CameraShakeData : ScriptableObject
    {
        public new string name;
        [Multiline]
        public string description;
        public float duration = 5f;
        public float strength = 1f;
        public int vibrato = 10;
        [Min(0f)] [Tooltip("Min 0 Max 180")]
        // Range ?
        public float randomness = 90f;
    }
}
