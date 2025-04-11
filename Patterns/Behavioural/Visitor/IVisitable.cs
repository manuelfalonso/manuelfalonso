namespace SombraStudios.Shared.Patterns.Behavioural.Visitor
{
    /// <summary>
    /// Interface for elements that can be visited by a visitor.
    /// </summary>
    public interface IVisitable
    {
        /// <summary>
        /// Accepts a visitor.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void Accept(IVisitor visitor);
    }
}
