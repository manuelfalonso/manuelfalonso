using SombraStudios.Shared.Extensions;
using UnityEngine;

namespace SombraStudios.Shared.Animations
{
    /// <summary>
    /// Controls Animation component giving extra utility methods.
    /// </summary>
    public class AnimationController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animation _animation;

        [Header("Debug")]
        [SerializeField] private bool _showLogs;


        #region Unity Messages
        private void Reset()
        {
            SafeInit();
        }

        private void Awake()
        {
            SafeInit();
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Plays the default animation.
        /// </summary>
        public void PlayDefaultAnimation()
        {
            if (!ValidateAnimationComponent()) return;

            _animation.Play();
        }

        /// <summary>
        /// Plays the specified animation by name.
        /// </summary>
        /// <param name="animationName">The name of the animation to play.</param>
        public void PlayAnimation(string animationName)
        {
            if (!ValidateAnimationComponent()) return;

            if (_animation.GetClip(animationName) == null)
            {
                Debug.LogError($"Animation {animationName} not found", this);
                return;
            }

            _animation.Play(animationName);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Safely initializes the animation component.
        /// </summary>
        private void SafeInit()
        {
            this.SafeInit(ref _animation);
        }

        /// <summary>
        /// Validates if the animation component is assigned.
        /// </summary>
        /// <returns>True if the animation component is assigned, otherwise false.</returns>
        private bool ValidateAnimationComponent()
        {
            if (_animation == null)
            {
                Debug.LogError("No animation component found", this);
                return false;
            }
            return true;
        }
        #endregion
    }
}
