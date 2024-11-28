using System;

namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Predicate based on a function delegate.
    /// </summary>
    public class FuncPredicate : IPredicate
    {
        /// <summary>
        /// The function delegate representing the predicate.
        /// </summary>
        private readonly Func<bool> _func;


        public FuncPredicate(Func<bool> predicate)
        {
            _func = predicate;
        }


        public bool Evaluate() => _func();
    }
}
