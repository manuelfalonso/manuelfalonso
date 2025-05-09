using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Physics.VelocityTracker
{
    /// <summary>  
    /// Tracks and smooths velocity over a specified duration using a sampling interval.  
    /// </summary>  
    public class VelocityTracker : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Range(0.1f, 5f)]
        [Tooltip("The duration over which velocity samples are tracked.")]
        private float _trackingDuration = 1f;

        [SerializeField, Range(0.001f, 1f)]
        [Tooltip("The interval at which velocity samples are taken.")]
        private float _sampleInterval = 0.1f;

        [SerializeField]
        [Tooltip("If true, the Y component of the velocity will be ignored when calculating the average velocity.")]
        private bool _ignoreY = false;

        private readonly Queue<Vector3> _velocityQueue = new();
        private float _lastSampleTime;
        private int _maxSamples;
        private Vector3 _velocitySum = Vector3.zero;

        /// <summary>  
        /// A function that provides the current velocity.  
        /// Must be assigned by the user.  
        /// </summary>  
        public Func<Vector3> VelocitySource { get; set; }

        /// <summary>  
        /// Gets the average velocity based on the historical data.  
        /// If no data is available, returns <see cref="Vector3.zero"/>.  
        /// </summary>  
        public Vector3 AverageVelocity
        {
            get
            {
                if (_velocityQueue.Count == 0)
                    return Vector3.zero;

                var avg = _velocitySum / _velocityQueue.Count;
                if (_ignoreY) avg.y = 0;

                return avg;
            }
        }

        /// <summary>  
        /// Initializes the maximum number of samples based on the tracking duration and sample interval.  
        /// </summary>  
        private void Awake()
        {
            _maxSamples = Mathf.CeilToInt(_trackingDuration / _sampleInterval);
        }

        /// <summary>  
        /// Updates the velocity tracker by sampling the velocity at the specified interval.  
        /// </summary>  
        private void Update()
        {
            SampleVelocity();
        }

        /// <summary>  
        /// Samples the current velocity and updates the historical data.  
        /// Removes the oldest sample if the maximum number of samples is reached.  
        /// </summary>  
        private void SampleVelocity()
        {
            if (VelocitySource == null)
            {
                return;
            }

            if (Time.time >= _lastSampleTime + _sampleInterval)
            {
                var newVelocity = VelocitySource.Invoke();

                if (_velocityQueue.Count == _maxSamples)
                {
                    _velocitySum -= _velocityQueue.Dequeue();
                }

                _velocityQueue.Enqueue(newVelocity);
                _velocitySum += newVelocity;

                _lastSampleTime = Time.time;
            }
        }
    }
}
