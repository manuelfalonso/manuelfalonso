using SombraStudios.Shared.Extensions;
using UnityEngine;

namespace SombraStudios.Shared.Animations
{
    /// <summary>
    /// Controls animations for a GameObject.
    /// </summary>
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Animation _animation;

        private void Reset()
        {
            this.SafeInit(ref _animation);
        }

        private void Awake()
        {
            this.SafeInit(ref _animation);
        }


        /// <summary>
        /// Plays the default animation.
        /// </summary>
        public void PlayDefaultAnimation()
        {
            if (_animation == null)
            {
                Debug.LogError("No default animation found");
                return;
            }

            _animation.Play();
        }
    }
}
