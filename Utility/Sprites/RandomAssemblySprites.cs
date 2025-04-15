using UnityEngine;

namespace SombraStudios.Shared.Utility.Sprites
{
    /// <summary>
    /// It could be attached to a NPC or Player to randomly generate characters.
    /// Manually assign sprites to each slot.
    /// </summary>
    public class RandomAssemblySprites : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer _headSpriteRenderer;
        [SerializeField] private SpriteRenderer _bodySpriteRenderer;
        [SerializeField] private SpriteRenderer _swordSpriteRenderer;
        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;

        [Header("Sprites")]
        [SerializeField] private Sprite[] _headSprites;
        [SerializeField] private Sprite[] _bodySprites;
        [SerializeField] private Sprite[] _swordSprites;
        [SerializeField] private Sprite[] _shieldSprites;

        private void Start()
        {
            AssignRandomSprites();
        }

        /// <summary>
        /// Randomly assign sprites to each sprite renderer.
        /// </summary>
        private void AssignRandomSprites()
        {
            _headSpriteRenderer.sprite = GetRandomSprite(_headSprites);
            _bodySpriteRenderer.sprite = GetRandomSprite(_bodySprites);
            _swordSpriteRenderer.sprite = GetRandomSprite(_swordSprites);
            _shieldSpriteRenderer.sprite = GetRandomSprite(_shieldSprites);
        }

        /// <summary>
        /// Gets a random sprite from the provided array.
        /// </summary>
        /// <param name="sprites">The array of sprites to choose from.</param>
        /// <returns>A randomly selected sprite from the array.</returns>
        private Sprite GetRandomSprite(Sprite[] sprites)
        {
            return sprites.Length > 0 ? sprites[Random.Range(0, sprites.Length)] : null;
        }
    }
}
