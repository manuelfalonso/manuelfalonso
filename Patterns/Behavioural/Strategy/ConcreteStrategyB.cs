using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Concrete Strategies implement different variations 
    /// of an algorithm the context uses.
    /// 
    /// Example for a Fire damage type
    /// </summary>
    public class ConcreteStrategyB : IStrategy
    {
        public void Execute(int damage)
        {
            // Do Fire damage
            Debug.Log($"{this} => Do {damage} fire damage");
        }
    }
}
