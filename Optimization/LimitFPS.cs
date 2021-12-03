using UnityEngine;

/// <summary>
/// Limit FPS to optimize performance
/// </summary>
public class LimitFps : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
