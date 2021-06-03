using UnityEngine;

public class RandomSpriteColor : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer sprite;
    public Color colorOne, colorTwo;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[Random.Range(0, sprites.Length)];
        sprite.color = Color.Lerp(colorOne, colorTwo, Random.value);
    }
}