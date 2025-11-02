using SombraStudios.Shared.Extensions;
using UnityEngine;

namespace SombraStudios.Shared.Examples.Graphs
{
    /// <summary>
    /// Represents a node in a graph with visual connections to neighboring nodes.
    /// Uses a LineRenderer to draw lines to connected nodes.
    /// </summary>
    public class GraphNode : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private GraphNode[] _neighbors;
        [SerializeField] private bool _isVisited;
        [Header("References")]
        [SerializeField] private LineRenderer _lineRenderer;

        public bool IsVisited
        {
            get => _isVisited;
            set => _isVisited = value;
        }

        public GraphNode[] Neighbors => _neighbors;

        private void Awake()
        {
            this.SafeInit(ref _lineRenderer);
        }

        private void Start()
        {
            SetupLineRenderer();
        }

        /// <summary>
        /// Configures the LineRenderer to draw lines from this node to all its neighbors.
        /// Sets line positions and renders them in 3D space.
        /// </summary>
        /// <remarks>
        /// Each connection requires two positions: this node's position and the neighbor's position.
        /// If the LineRenderer is not present, logs an error and returns without making changes.
        /// </remarks>
        private void SetupLineRenderer()
        {
            if (_lineRenderer == null)
            {
                Debug.LogError("LineRenderer component is missing.", this);
                return;
            }

            var neighborsCount = _neighbors.Length;
            _lineRenderer.positionCount = neighborsCount * 2;

            var currentPosition = transform.position;
            var positions = new Vector3[neighborsCount * 2];

            for (int i = 0; i < neighborsCount; i++)
            {
                var baseIndex = i * 2;
                positions[baseIndex] = currentPosition;
                positions[baseIndex + 1] = _neighbors[i].transform.position;
            }

            _lineRenderer.SetPositions(positions);
        }
    }
}
