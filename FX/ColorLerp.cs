using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    private SpriteRenderer sprite;
    Color lerpedColor = Color.white;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        sprite.color = lerpedColor;
    }
}