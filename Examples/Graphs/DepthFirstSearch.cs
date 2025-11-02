using UnityEngine;

namespace SombraStudios.Shared.Examples.Graphs
{
    /// <summary>
    /// Implements the Depth-First Search (DFS) algorithm for graph traversal.
    /// </summary>
    public class DepthFirstSearch : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GraphNode _startNode;
        [SerializeField] private int _maxDepth = 1000;

        private void Start()
        {
            if (_startNode == null)
            {
                Debug.LogError("Start node is not assigned.", this);
                return;
            }

            Debug.Log("Starting DFS Traversal");
            DFSRecursive(_startNode, 0);
        }

        private void DFSRecursive(GraphNode currentNode, int depth)
        {
            if (currentNode == null || currentNode.IsVisited || depth >= _maxDepth)
                return;

            currentNode.IsVisited = true;
            Debug.Log($"Visited Node: {currentNode.name}");

            var neighbors = currentNode.Neighbors;
            var neighborsCount = neighbors.Length;

            for (int i = 0; i < neighborsCount; i++)
            {
                var neighbor = neighbors[i];
                if (neighbor != null && !neighbor.IsVisited)
                {
                    DFSRecursive(neighbor, depth + 1);
                }
            }
        }
    }
}
