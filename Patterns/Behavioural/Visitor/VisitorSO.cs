using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Visitor
{
    /// <summary>
    /// Abstract base class for visitors implemented as ScriptableObjects.
    /// </summary>
    public abstract class VisitorSO : ScriptableObject, IVisitor
    {
        /// <summary>
        /// Visits a visitable element.
        /// </summary>
        /// <param name="visitable">The visitable element to visit.</param>
        public abstract void Visit(IVisitable visitable);
    }

    /// <summary>
    /// Generic abstract base class for visitors that visit specific types of visitable elements.
    /// </summary>
    /// <typeparam name="T">The type of visitable element.</typeparam>
    public abstract class VisitorSO<T> : VisitorSO where T : MonoBehaviour, IVisitable
    {
        /// <summary>
        /// Visits a visitable element if it is of the specified type.
        /// </summary>
        /// <param name="visitable">The visitable element to visit.</param>
        public override void Visit(IVisitable visitable)
        {
            if (visitable is T typedVisitable)
            {
                VisitTyped(typedVisitable);
            }
        }

        /// <summary>
        /// Visits a visitable element of the specified type.
        /// </summary>
        /// <param name="visitable">The visitable element to visit.</param>
        protected abstract void VisitTyped(T visitable);
    }
}
