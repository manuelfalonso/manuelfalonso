using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Class to set the parent of a GameObject and apply position and rotation offsets.
    /// </summary>
    public class ParentSetter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _parentTransform;

        [Header("Offsets")]
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private Vector3 _initialLocalPosition;
        private Quaternion _initialLocalRotation;

        /// <summary>
        /// Stores the initial local position and rotation of the GameObject.
        /// </summary>
        private void Awake()
        {
            _initialLocalPosition = transform.localPosition;
            _initialLocalRotation = transform.localRotation;
        }

        /// <summary>
        /// Sets the parent of the GameObject to the specified transform and applies the position and rotation offsets.
        /// </summary>
        /// <param name="newParent">The new parent transform.</param>
        public void SetParent(Transform newParent)
        {
            if (newParent == null) return;

            transform.SetParent(newParent, false);
            transform.SetLocalPositionAndRotation(
                _initialLocalPosition + _positionOffset,
                _initialLocalRotation * Quaternion.Euler(_rotationOffset)
            );
        }

        /// <summary>
        /// Sets the parent of the GameObject to the default parent transform specified in the inspector and 
        /// applies the position and rotation offsets.
        /// </summary>
        public void SetParent()
        {
            SetParent(_parentTransform);
        }
    }
}
