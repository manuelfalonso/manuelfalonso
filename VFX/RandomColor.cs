using UnityEngine;

namespace SombraStudios.VFX
{
    /// <summary>
    /// Asign a random color to the material.
    /// </summary>
    public class RandomColor : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV(0, 1);
        }
    }
}