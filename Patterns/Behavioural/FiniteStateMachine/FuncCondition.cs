using SombraStudios.Shared.Interfaces;
using System;

namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Condition backed by a function delegate.
    /// </summary>
    public class FuncCondition : ICondition
    {
        /// <summary>
        /// The function delegate evaluated by this condition.
        /// </summary>
        private readonly Func<bool> _func;


        /// <summary>
        /// Initializes a new condition backed by the given delegate.
        /// </summary>
        /// <param name="condition">The delegate evaluated on each check.</param>
        public FuncCondition(Func<bool> condition)
        {
            _func = condition;
        }

        /// <summary>
        /// Invokes the delegate and returns its result.
        /// </summary>
        /// <returns>True if the condition is met, false otherwise.</returns>
        public bool IsValid() => _func();
    }
}
