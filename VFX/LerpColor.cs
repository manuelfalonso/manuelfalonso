using UnityEngine;

/// <summary>
/// Interpolate between an initial and a final color of the material.
/// </summary>
public class LerpColor : MonoBehaviour
{
    [SerializeField]
    private Color _initialColor = Color.white;
    [SerializeField]
    private Color _finalColor = Color.black;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Color lerpedColor = Color.Lerp(_initialColor, _finalColor, Mathf.PingPong(Time.time, 1));
        _renderer.material.color = lerpedColor;
    }
}