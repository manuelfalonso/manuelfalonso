using UnityEngine;

namespace SombraStudios.Shared.VFX.CameraShake
{
    /// <summary>
    /// Store Shake Data to be used with DoTween DOShakePosition method
    /// </summary>
    [CreateAssetMenu(fileName = "Shake Data", menuName = "Sombra Studios/Camera/Shake", order = 51)]
    public class CameraShakeDataSO : ScriptableObject
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
