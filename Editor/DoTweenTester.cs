#if DOTWEEN
using UnityEngine;

namespace SombraStudios.Shared.Editor
{

    /// <summary>
    /// Implement abstract method to be able to test the Tween on Editor
    /// </summary>
    public abstract class DoTweenTester : MonoBehaviour
    {
        public abstract DG.Tweening.Tween GetTween();
    }
}
#endif