using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Examples.Graphs
{
    // BFS Algorithm Implementation
    public class BreadthFirstSearch : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GraphNode _startNode;

        private void Start()
        {
            if (_startNode == null)
            {
                Debug.LogError("Start node is not assigned.", this);
                return;
            }

            Debug.Log("Starting BFS Traversal");
            BFS(_startNode);
        }

        /// <summary>
        /// Performs Breadth-First Search traversal starting from the specified node.
        /// Visits nodes level by level, ensuring all nodes at depth n are visited before depth n+1.
        /// </summary>
        /// <param name="startNode">The starting node for BFS traversal.</param>
        private void BFS(GraphNode startNode)
        {
            if (startNode == null || startNode.IsVisited)
                return;

            var queue = new Queue<GraphNode>();

            EnqueueAndMarkAsVisited(startNode, queue);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode == null)
                    continue;

                var neighbors = currentNode.Neighbors;
                var neighborsCount = neighbors.Length;

                for (int i = 0; i < neighborsCount; i++)
                {
                    var neighbor = neighbors[i];

                    if (neighbor != null && !neighbor.IsVisited)
                    {
                        EnqueueAndMarkAsVisited(neighbor, queue);
                    }
                }
            }
        }

        private static void EnqueueAndMarkAsVisited(GraphNode node, Queue<GraphNode> queue)
        {
            node.IsVisited = true;
            queue.Enqueue(node);
            Debug.Log($"Visited Node: {node.name}");
        }
    }
}
