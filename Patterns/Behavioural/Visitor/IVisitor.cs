namespace SombraStudios.Shared.Patterns.Behavioural.Visitor
{
    /// <summary>
    /// Interface for visitors that can visit visitable elements.
    /// </summary>
    public interface IVisitor
    {
        /// <summary>
        /// Visits a visitable element.
        /// </summary>
        /// <param name="visitable">The visitable element to visit.</param>
        void Visit(IVisitable visitable);
    }
}
