#if DOTWEEN
using DG.Tweening;
using SombraStudios.Shared.Interfaces;
using UnityEngine;

namespace SombraStudios.Shared.VFX.CameraShake
{
    /// <summary>
    /// Store Shake Data to be used with DoTween DOShakePosition method
    /// </summary>
    [CreateAssetMenu(fileName = "ShakeData", menuName = "Sombra Studios/Camera/Shake Data")]
    public class CameraShakeDataSO : ScriptableObject, IDescribable
    {
        [SerializeField, Multiline] private string _description;
        
        [Header("Settings")]
        
        [Min(0f)]
        public float Duration = .5f;
        
        [Tooltip("The shake strength for each axis.")]
        public Vector3 Strength = Vector3.zero;
        
        [Min(0f)]
        [Tooltip("How much will the shake vibrate.")]
        public int Vibrato = 10;
        
        [Range(0f, 180f)]
        [Tooltip("How much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). " +
            "Setting it to 0 will shake along a single direction.")]
        public float Randomness = 10f;
        
        [Tooltip("If TRUE the shake will automatically fadeOut smoothly within the tween's duration, " +
            "otherwise it will not.")]
        public bool FadeOut = true;
        
        [Tooltip("The type of randomness to apply, Full (fully random) or Harmonic " +
            "(more balanced and visually more pleasant).")]
        public ShakeRandomnessMode ShakeRandomnessMode = ShakeRandomnessMode.Harmonic;

        public string Description { get => _description; set => _description = value; }
    }
}
#endif
