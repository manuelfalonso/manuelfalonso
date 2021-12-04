using UnityEngine;

/// <summary>
/// It could be attach to a NPC or Player to randomly generate characters 
/// Manually assign sprites to each slot.
/// </summary>
public class RandomAssemblySprites : MonoBehaviour
{
	[SerializeField]
	private Sprite[] headSprites;
	[SerializeField]
	private Sprite[] bodySprites;
	[SerializeField]
	private Sprite[] swordSprites;
	[SerializeField]
	private Sprite[] shieldSprites;

	[SerializeField]
	private SpriteRenderer headSpriteRenderer;
	[SerializeField]
	private SpriteRenderer bodySpriteRenderer;
	[SerializeField]
	private SpriteRenderer swordSpriteRenderer;
	[SerializeField]
	private SpriteRenderer shieldSpriteRenderer;

    private void Start()
    {
		AssignRandomSprites();
	}

	/// <summary>
	/// Randombly assign sprites to each sprite renderer
	/// </summary>
	private void AssignRandomSprites()
    {
		headSpriteRenderer.sprite = headSprites[Random.Range(0, headSprites.Length)];
		bodySpriteRenderer.sprite = bodySprites[Random.Range(0, bodySprites.Length)];
		swordSpriteRenderer.sprite = swordSprites[Random.Range(0, swordSprites.Length)];
		shieldSpriteRenderer.sprite = shieldSprites[Random.Range(0, shieldSprites.Length)];
    }
}
