using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.VFX
{
    /// <summary>
    /// Line Renderer positioner that use World Position to update the line positions.
    /// </summary>
    public class LinePositionsUpdater : MonoBehaviour
    {
        [SerializeField] private bool _isActive;

        [Header("References")]
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("Data")]
        [SerializeField] private List<Transform> _positions = new List<Transform>();

        public bool IsActive { get => _isActive; set => _isActive = value; }


        void Start()
        {
            if (_lineRenderer == null)
            {
                Debug.LogError($"{this}: Missing references");
                enabled = false;
            }

            if (_lineRenderer.useWorldSpace == false)
            {
                Debug.LogWarning($"Setting Use World Space for this LineRenderer", this);
                _lineRenderer.useWorldSpace = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_isActive) { return; }

            _lineRenderer.SetPositions(GetPositions());
        }


        public void SetPositions(List<Transform> positions)
        {
            if (positions == null) { return; }
            if (_positions == null) { _positions = new List<Transform>(); }

            _positions.Clear();
            _positions.AddRange(positions);
        }

        public void ReplacePosition(int index, Transform position)
        {
            if (position == null) { return; }

            if (_positions.Count > index)
            {
                _positions[index] = position;
            }
            else
            {
                InsertPosition(index, position);
            }
        }

        public void InsertPosition(int index, Transform position)
        {
            if (position == null) { return; }
            _positions.Insert(index, position);
        }


        private Vector3[] GetPositions()
        {
            var positions = new Vector3[_positions.Count];
            for (int i = 0; i < _positions.Count; i++)
            {
                if (_positions[i] == null) { continue; }

                positions[i] = _positions[i].position;
            }
            return positions;
        }
    }
}
