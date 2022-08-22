using UnityEngine;
using DG.Tweening;

/// <summary>
/// Simple controller to use camera shake with DoTween with Scriptable Objects
/// </summary>
public class CameraShakeController : MonoBehaviour
{
    public void CameraShake(Camera camera, CameraShakeData shakeData)
    {
        if (shakeData == null)
            return;

        if (camera == null)
            camera = Camera.main;

        camera.DOShakePosition(shakeData.duration, shakeData.strength, shakeData.vibrato, shakeData.randomness);
    }

    public void CameraShake(CameraShakeData shakeData)
    {
        if (shakeData == null)
            return;

        Camera.main.DOShakePosition(shakeData.duration, shakeData.strength, shakeData.vibrato, shakeData.randomness);
    }
}
