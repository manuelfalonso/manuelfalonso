using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Concrete Strategies implement different variations 
    /// of an algorithm the context uses.
    /// 
    /// Example for a Ice damage type
    /// </summary>
    public class ConcreteStrategyA : IStrategy
    {
        public bool CanExecute(int damage)
        {
            return damage > 0;
        }

        public void Execute(int damage)
        {
            // Do Ice damage
            Debug.Log($"{this} => Do {damage} ice damage");
        }

        public bool TryToExecute(int damage)
        {
            if (!CanExecute(damage)) { return false; }
            else
            {
                Execute(damage);
                return true;
            }
        }
    }
}
