using SombraStudios.Extensions;
using System;
using UnityEngine;
using UnityEngine.Splines;

namespace SombraStudios.Utils
{
    /// <summary>
    /// Animates objects using Unity Splines package and Animation Curves
    /// Works either with Objects and UI
    /// Requieres:
    /// UnityEngine.Splines
    /// Documentation: https://docs.unity3d.com/Packages/com.unity.splines@2.4/manual/index.html
    /// </summary>
    [RequireComponent(typeof(SplineAnimate))]
    public class SplineController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SplineContainer _splineContainer = null;
        [SerializeField] private SplineAnimate _splineAnimation = null;

        [Header("Scale")]
        [SerializeField] private bool _animateScale = false;
        [SerializeField] private AnimationCurve _scaleCurve = null;

        [Header("Debug")]
        [SerializeField] private bool _showLogs = false;
        [SerializeField] private bool _isActive = true;

        public bool IsActive { get => _isActive; set => _isActive = value; }

        // EVENTS
        public event Action AnimationStarted;
        public event Action<Vector3, Quaternion, Vector3> AnimationUpdated;
        public event Action AnimationStopped;
        public event Action AnimationReseted;
        public event Action AnimationCompleted;


        private void OnEnable()
        {
            if (_splineAnimation == null) { return; }
            _splineAnimation.Updated -= OnAnimationUpdated;
            _splineAnimation.Updated += OnAnimationUpdated;
        }

        private void Start()
        {
            this.SafeInit(ref _splineAnimation);

            if (_splineAnimation == null) { return; }
            if (_splineAnimation.PlayOnAwake == true) { OnAnimationStarted(); }
        }

        private void OnDisable()
        {
            if (_splineAnimation == null) { return; }
            _splineAnimation.Updated -= OnAnimationUpdated;
        }


        /// <summary>
        /// Plays current Animation
        /// </summary>
        public void PlayAnimation()
        {
            if (_splineAnimation == null) { return; }
            if (!_isActive) { return; }
            _splineAnimation.Play();
            OnAnimationStarted();
        }

        /// <summary>
        /// Plays current Animation from the normalized <paramref name="progressionValue"/> time
        /// </summary>
        /// <param name="progressionValue"></param>
        public void PlayAnimation(float progressionValue)
        {
            if (_splineContainer == null) { return; }
            if (!_isActive) { return; }
            if (progressionValue <0 || progressionValue > 1)
            {
                Debug.LogWarning($"Progression value must be normalized between 0 and 1.", this);
                return;
            }
            _splineAnimation.NormalizedTime = progressionValue;
            _splineAnimation.Play();
            OnAnimationStarted();
        }

        /// <summary>
        /// Stops current Animation
        /// </summary>
        public void StopAnimation()
        {
            if (_splineAnimation == null) { return; }
            if (!_isActive) { return; }
            _splineAnimation.Pause();
            OnAnimationStopped();
        }

        /// <summary>
        /// Resets current Animation
        /// </summary>
        public void ResetAnimation()
        {
            if (_splineAnimation == null) { return; }
            if (!_isActive) { return; }
            _splineAnimation.Restart(false);
            OnAnimationReseted();
        }


        private void UpdateScale()
        {
            if (!_animateScale) { return; }
            if (_scaleCurve.keys.Length == 0) { return; }

            var lerpInitialValue = _scaleCurve.keys[0].time;
            var lerpFinalValue = _scaleCurve.keys[_scaleCurve.length - 1].time;
            var lerpTime = _splineAnimation.NormalizedTime;

            var interpolatedValue = Mathf.Lerp(lerpInitialValue, lerpFinalValue, lerpTime);
            var curveValue = _scaleCurve.Evaluate(interpolatedValue);
            var newScale = Vector3.one * curveValue;

            transform.localScale = newScale;
        }

        #region Events
        private void OnAnimationStarted()
        {
            AnimationStarted?.Invoke();
            Tools.Logger.Log(_showLogs, $"AnimationStarted", this);
        }

        private void OnAnimationUpdated(Vector3 position, Quaternion rotation)
        {
            UpdateScale();
            AnimationUpdated?.Invoke(position, rotation, transform.localScale);
            Tools.Logger.Log(_showLogs, $"AnimationUpdated", this);

            if (_splineAnimation.NormalizedTime == 1f) { OnAnimationCompleted(); }
        }

        private void OnAnimationStopped()
        {
            AnimationStopped?.Invoke();
            Tools.Logger.Log(_showLogs, $"AnimationStopped", this);
        }

        private void OnAnimationReseted()
        {
            AnimationReseted?.Invoke();
            Tools.Logger.Log(_showLogs, $"AnimationReseted", this);
        }

        private void OnAnimationCompleted()
        {
            AnimationCompleted?.Invoke();
            Tools.Logger.Log(_showLogs, $"AnimationCompleted", this);
        }
        #endregion
    }
}
