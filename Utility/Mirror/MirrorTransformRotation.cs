using SombraStudios.Shared.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Mirror
{
    /// <summary>
    /// This class mirrors the rotation of specified targets along a given axis.
    /// </summary>
    public class MirrorTransformRotation : MonoBehaviour
    {
        /// <summary>
        /// The axis along which to mirror the rotation.
        /// </summary>
        [SerializeField] private Axis _axes = Axis.X;

        /// <summary>
        /// The list of target transforms to be mirrored.
        /// </summary>
        [SerializeField] private List<Transform> _targets;

        /// <summary>
        /// Whether to mirror the rotations at the start.
        /// </summary>
        [SerializeField] private bool _mirrorAtStart = false;


        private void Start()
        {
            if (_mirrorAtStart)
            {
                MirrorRotation();
            }
        }


        /// <summary>
        /// Mirrors the rotations of the target transforms along the specified axis.
        /// </summary>
        public void MirrorRotation()
        {
            foreach (var target in _targets)
            {
                Vector3 localEulerAngles = target.localEulerAngles;

                if (_axes.HasFlag(Axis.X))
                {
                    localEulerAngles.x = -localEulerAngles.x;
                }

                if (_axes.HasFlag(Axis.Y))
                {
                    localEulerAngles.y = -localEulerAngles.y;
                }

                if (_axes.HasFlag(Axis.Z))
                {
                    localEulerAngles.z = -localEulerAngles.z;
                }

                target.localEulerAngles = localEulerAngles;
            }
        }
    }
}
