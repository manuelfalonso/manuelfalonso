using SombraStudios.Shared.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Mirror
{
    /// <summary>
    /// This class mirrors the scale of specified targets along a given axis.
    /// </summary>
    public class MirrorTransformScale : MonoBehaviour
    {
        /// <summary>
        /// The axis along which to mirror the scale.
        /// </summary>
        [SerializeField] private Axis _axis = Axis.X;

        /// <summary>
        /// The list of target transforms to be mirrored.
        /// </summary>
        [SerializeField] private List<Transform> _targets;

        /// <summary>
        /// Whether to mirror the scale at the start.
        /// </summary>
        [SerializeField] private bool _mirrorAtStart = false;


        private void Start()
        {
            if (_mirrorAtStart)
            {
                MirrorScale();
            }
        }


        /// <summary>
        /// Mirrors the scales of the target transforms along the specified axis.
        /// </summary>
        public void MirrorScale()
        {
            foreach (var target in _targets)
            {
                if (ContainsCollider(target))
                {
                    continue;
                }

                Vector3 localScale = target.localScale;

                if (_axis.HasFlag(Axis.X))
                {
                    localScale.x = -localScale.x;
                }

                if (_axis.HasFlag(Axis.Y))
                {
                    localScale.y = -localScale.y;
                }

                if (_axis.HasFlag(Axis.Z))
                {
                    localScale.z = -localScale.z;
                }

                target.localScale = localScale;
            }
        }


        private bool ContainsCollider(Transform target)
        {
            Collider collider = target.GetComponentInChildren<Collider>(true);

            if (collider != null)
            {
                Debug.LogError("Colliders does not support negative scale or size. " + collider.name, gameObject);
                return true;
            }

            return false;
        }
    }
}
