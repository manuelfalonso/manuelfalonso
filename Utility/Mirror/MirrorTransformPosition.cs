using SombraStudios.Shared.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Mirror
{
    /// <summary>
    /// This class mirrors the position of specified targets along a given axis.
    /// </summary>
    public class MirrorTransformPosition : MonoBehaviour
    {
        /// <summary>
        /// The axis along which to mirror the position.
        /// </summary>
        [SerializeField] private Axis _axes = Axis.X;

        /// <summary>
        /// The list of target transforms to be mirrored.
        /// </summary>
        [SerializeField] private List<Transform> _targets;

        /// <summary>
        /// Whether to mirror the positions at the start.
        /// </summary>
        [SerializeField] private bool _mirrorAtStart = false;


        private void Start()
        {
            if (_mirrorAtStart)
            {
                Mirror();
            }
        }


        /// <summary>
        /// Mirrors the positions of the target transforms along the specified axis.
        /// </summary>
        public void Mirror()
        {
            foreach (var target in _targets)
            {
                var position = target.localPosition;

                if (_axes.HasFlag(Axis.X))
                {
                    position.x = -position.x;
                }

                if (_axes.HasFlag(Axis.Y))
                {
                    position.y = -position.y;
                }

                if (_axes.HasFlag(Axis.Z))
                {
                    position.z = -position.z;
                }

                target.localPosition = position;
            }
        }
    }
}
