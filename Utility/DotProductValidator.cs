using SombraStudios.Attributes;
using SombraStudios.Utility.UnityGizmos;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Validator for two tranforms considering desired Direction for Dot Product operation
    /// </summary>
    public class DotProductValidator : MonoBehaviour
    {
        [Header("Vectors")]
        [SerializeField] private Transform _transformOne;
        [SerializeField] private Direction _directionOne;
        [SerializeField] private Transform _transformTwo;
        [SerializeField] private Direction _directionTwo;

        [Header("Config")]
        [SerializeField] private bool _automaticValidate = true;
        [Tooltip("Time in seconds to validate dot product")]
        [SerializeField] private float _tickTime = 0.1f;

        [Header("Debug")]
        [SerializeField, ReadOnly] protected float _currentDotProduct = 0f;
        [SerializeField] private bool _showLogs;
        [SerializeField] private bool _showGizmo;
        [SerializeField] private GizmosLineUtility _directionOneGizmo;
        [SerializeField] private GizmosLineUtility _directionTwoGizmo;

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


        public virtual float CalculateDotProduct()
        {
            var dotProduct = 0f;

            var vectorOneDirection = GetVectorFromDirection((Direction)_directionOne);
            if (vectorOneDirection == Vector3.zero) { return dotProduct; }
            var vectorOne = _transformOne.TransformDirection(vectorOneDirection);
            Tools.Logger.Log(_showLogs, $"Vector One: {vectorOne}", this);

            var vectorTwoDirection = GetVectorFromDirection((Direction)_directionTwo);
            if (vectorTwoDirection == Vector3.zero) { return dotProduct; }
            var vectorTwo = _transformTwo.TransformDirection(vectorTwoDirection);
            Tools.Logger.Log(_showLogs, $"Vector Two: {vectorTwo}", this);

            SetLineGizmos(_directionOneGizmo, vectorOne);
            SetLineGizmos(_directionTwoGizmo, vectorTwo);

            CurrentDotProduct = Mathf.Round(Vector3.Dot(vectorOne, vectorTwo) * 100f) / 100f;
            Tools.Logger.Log(_showLogs, $"Current Dot Product: {_currentDotProduct}", this);

            dotProduct = CurrentDotProduct;
            return dotProduct;
        }


        private IEnumerator RunTicks()
        {
            if (!_automaticValidate) { yield break; }

            while (true)
            {
                yield return _tickDelay;
                CalculateDotProduct();
            }
        }

        private Vector3 GetVectorFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector3.up;
                case Direction.Down:
                    return Vector3.down;
                case Direction.Left:
                    return Vector3.left;
                case Direction.Right:
                    return Vector3.right;
                case Direction.Foward:
                    return Vector3.forward;
                case Direction.Back:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
        }

        private void SetLineGizmos(GizmosLineUtility line, Vector3 toDirection)
        {
            if (!_showGizmo) { return; }
            if (line == null) { return; }
            line.To = toDirection;
        }
    }

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
