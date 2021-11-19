using UnityEngine;

/// <summary>
/// Simple Instruction to destroy a GameObject after a delay.
/// </summary>
public class DestroyAfterDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }
}
