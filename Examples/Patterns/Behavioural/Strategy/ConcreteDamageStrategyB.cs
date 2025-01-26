using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Concrete Strategies implement different variations 
    /// of an algorithm the context uses.
    /// 
    /// Example for a Fire damage type
    /// </summary>
    public class ConcreteDamageStrategyB : IDamageStrategy
    {
        public bool CanExecute(int damage)
        {
            return damage > 0;
        }

        public void Execute(int damage)
        {
            // Do Fire damage
            Debug.Log($"{this} => Do {damage} fire damage");
        }

        public bool TryToExecute(int damage)
        {
            if (!CanExecute(damage)) return false;
            Execute(damage);
            return true;
        }
    }
}
