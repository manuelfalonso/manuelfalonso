#if DOTWEEN
using DG.Tweening;
using SombraStudios.Shared.ScriptableObjects.Patterns.Behavioural.Strategy;
using UnityEngine;

namespace SombraStudios.Shared.VFX.CameraShake
{
    /// <summary>
    /// Scriptable object for a main camera shake effect which can be of Position and/or Rotation.
    /// </summary>
    [CreateAssetMenu(fileName = "CameraShakeStrategy", menuName = "Sombra Studios/Strategies/VFX/Camera/Camera Shake")]
    public class CameraShakeStrategy : StrategySO<CameraShakeDataSO>
    {
        [Header("Settings")]
        public bool ShakePosition = true;
        public bool ShakeRotation = false;

        public override bool CanExecute(CameraShakeDataSO data)
        {
            if (base.CanExecute(data) == false) return false;

            // Check if the camera shake data is valid
            if (data == null)
            {
                Debug.LogError("CameraShakeDataSO is null.");
                return false;
            }

            // Check if the camera shake data has a valid duration
            if (data.Duration <= 0)
            {
                Debug.LogError("CameraShakeDataSO duration must be greater than 0.");
                return false;
            }

            return true;            
        }

        public override void Execute(CameraShakeDataSO data)
        {
            if (ShakePosition)
                Camera.main.DOShakePosition(
                    data.Duration, data.Strength, data.Vibrato, data.Randomness, data.FadeOut, data.ShakeRandomnessMode);

            if (ShakeRotation)
                Camera.main.DOShakeRotation(
                    data.Duration, data.Strength, data.Vibrato, data.Randomness, data.FadeOut, data.ShakeRandomnessMode);
        }
    }
}
#endif
