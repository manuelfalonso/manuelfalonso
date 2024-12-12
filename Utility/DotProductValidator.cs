using SombraStudios.Shared.Attributes;
using SombraStudios.Shared.Utility.UnityGizmos;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Validator for two tranforms considering desired Direction for Dot Product operation
    /// </summary>
    public class DotProductValidator : MonoBehaviour
    {
        [Header("Vectors")]
        /// <summary>
        /// The first transform for dot product calculation.
        /// </summary>
        [Tooltip("The first transform for dot product calculation.")]
        [SerializeField] private Transform _transformOne;
        /// <summary>
        /// The direction for the first transform.
        /// </summary>
        [Tooltip("The direction for the first transform.")]
        [SerializeField] private Direction _directionOne;
        /// <summary>
        /// Flag to determine whether to compare with a transform or not.
        /// </summary>
        [Tooltip("Flag to determine whether to compare with a transform or not.")]
        [SerializeField] private bool _compareWithTransform = true;
        /// <summary>
        /// The second transform for dot product calculation.
        /// </summary>
        [Tooltip("The second transform for dot product calculation.")]
        [SerializeField] private Transform _transformTwo;
        /// <summary>
        /// The direction for the second transform.
        /// </summary>
        [Tooltip("The direction for the second transform.")]
        [SerializeField] private Direction _directionTwo;

        [Header("Config")]
        /// <summary>
        /// Flag to determine if dot product validation should occur automatically.
        /// </summary>
        [Tooltip("Flag to determine if dot product validation should occur automatically.")]
        [SerializeField] private bool _automaticValidate = true;
        /// <summary>
        /// Time in seconds to validate dot product
        /// </summary>
        [Tooltip("Time in seconds to validate dot product")]
        [SerializeField] private float _tickTime = 0.1f;

        [Header("Debug")]
        /// <summary>
        /// The current dot product value.
        /// </summary>
        [Tooltip("The current dot product value.")]
        [SerializeField, ReadOnly] protected float _currentDotProduct = 0f;
        /// <summary>
        /// Flag to show debug logs.
        /// </summary>
        [Tooltip("Flag to show debug logs.")]
        [SerializeField] private bool _showLogs;
        /// <summary>
        /// Flag to show gizmos.
        /// </summary>
        [Tooltip("Flag to show gizmos.")]
        [SerializeField] private bool _showGizmo;
        /// <summary>
        /// The gizmo utility for the first direction.
        /// </summary>
        [Tooltip("The gizmo utility for the first direction.")]
        [SerializeField] private GizmosLineUtility _directionOneGizmo;
        /// <summary>
        /// The gizmo utility for the second direction.
        /// </summary>
        [Tooltip("The gizmo utility for the second direction.")]
        [SerializeField] private GizmosLineUtility _directionTwoGizmo;

        /// <summary>
        /// The current dot product value.
        /// </summary>
        public float CurrentDotProduct
        {
            get
            {
                return _currentDotProduct;
            }
            private set
            {
                if (value == _currentDotProduct) { return; }
                _currentDotProduct = value;
                DotProductChanged?.Invoke(_currentDotProduct);
            }
        }

        /// <summary>
        /// Event invoked when the dot product changes.
        /// </summary>
        public UnityEvent<float> DotProductChanged;

        private WaitForSeconds _tickDelay;


        private void Awake()
        {
            _tickDelay = new WaitForSeconds(_tickTime);
        }

        private void OnEnable()
        {
            StartCoroutine(RunTicks());
        }

        private void Start()
        {
            if (_directionOneGizmo != null) { _directionOneGizmo.GizmosEnabled = _showGizmo; }
            if (_directionTwoGizmo != null) { _directionTwoGizmo.GizmosEnabled = _showGizmo; }
        }


        /// <summary>
        /// Calculates the dot product.
        /// </summary>
        public virtual float CalculateDotProduct()
        {
            var dotProduct = 0f;

            var vectorOneDirection = GetVectorFromDirection(_directionOne);
            if (vectorOneDirection == Vector3.zero) { return dotProduct; }
            var vectorOne = _transformOne.TransformDirection(vectorOneDirection);
            
            if (_showLogs)
                Loggers.Logger.Log($"Vector One: {vectorOne}", this);

            var vectorTwoDirection = GetVectorFromDirection(_directionTwo);
            if (vectorTwoDirection == Vector3.zero) { return dotProduct; }
            var vectorTwo = Vector3.zero;
            if (_compareWithTransform)
            {
                vectorTwo = _transformTwo.TransformDirection(vectorTwoDirection);
            }
            else
            {
                vectorTwo = GetVectorFromDirection(_directionTwo);
            }
            
            if (_showLogs)
                Loggers.Logger.Log($"Vector Two: {vectorTwo}", this);

            SetLineGizmos(_directionOneGizmo, vectorOne);
            SetLineGizmos(_directionTwoGizmo, vectorTwo);

            CurrentDotProduct = Vector3.Dot(vectorOne, vectorTwo);
            
            if (_showLogs)
                Loggers.Logger.Log($"Current Dot Product: {_currentDotProduct}", this);

            dotProduct = CurrentDotProduct;
            return dotProduct;
        }


        /// <summary>
        /// Runs continuous dot product calculations.
        /// </summary>
        private IEnumerator RunTicks()
        {
            if (!_automaticValidate) { yield break; }

            while (true)
            {
                yield return _tickDelay;
                CalculateDotProduct();
            }
        }

        /// <summary>
        /// Retrieves the vector from a specified direction.
        /// </summary>
        private Vector3 GetVectorFromDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Vector3.up,
                Direction.Down => Vector3.down,
                Direction.Left => Vector3.left,
                Direction.Right => Vector3.right,
                Direction.Foward => Vector3.forward,
                Direction.Back => Vector3.back,
                _ => Vector3.zero,
            };
        }

        /// <summary>
        /// Sets the gizmo lines.
        /// </summary>
        private void SetLineGizmos(GizmosLineUtility line, Vector3 toDirection)
        {
            if (!_showGizmo) { return; }
            if (line == null) { return; }
            line.To = toDirection;
        }
    }

    /// <summary>
    /// Enumeration of possible Vector3 directions.
    /// </summary>
    public enum Direction
    {
        Up = 1,
        Down,
        Left,
        Right,
        Foward,
        Back
    }
}
