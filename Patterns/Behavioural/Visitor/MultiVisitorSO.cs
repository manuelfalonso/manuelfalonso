using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Visitor
{
    /// <summary>
    /// ScriptableObject that allows multiple visitors to visit a visitable element.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMultiVisitor", 
        menuName = "Sombra Studios/Patterns/Behavioural/Visitor/Multi Visitor")]
    public class MultiVisitorSO : VisitorSO
    {
        [Header("Settings")]
        [SerializeField] private List<VisitorSO> _visitors = new();

        /// <summary>
        /// Allows each visitor in the list to visit the provided visitable element.
        /// </summary>
        /// <param name="visitable">The visitable element to be visited.</param>
        public override void Visit(IVisitable visitable)
        {
            foreach (var visitor in _visitors)
            {
                if (visitor == null)
                {
                    Debug.LogWarning($"Visitor {name} is null");
                    continue;
                }
                visitor.Visit(visitable);
            }
        }
    }

    /// <summary>
    /// Generic ScriptableObject that allows multiple typed visitors to visit a visitable element of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of visitable element.</typeparam>
    public abstract class MultiVisitorSO<T> : VisitorSO<T> where T : MonoBehaviour, IVisitable
    {
        [SerializeField] private List<VisitorSO<T>> _typedVisitors = new();

        /// <summary>
        /// Allows each typed visitor in the list to visit the provided visitable element of the specified type.
        /// </summary>
        /// <param name="visitable">The visitable element to be visited.</param>
        protected override void VisitTyped(T visitable)
        {
            foreach (var visitor in _typedVisitors)
            {
                if (visitor == null)
                {
                    Debug.LogWarning($"Visitor {name} is null");
                    continue;
                }
                visitor.Visit(visitable);
            }
        }
    }
}
