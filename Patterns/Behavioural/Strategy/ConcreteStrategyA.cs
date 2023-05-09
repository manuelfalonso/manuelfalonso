namespace SombraStudios.Patterns.Behavioural
{

    using UnityEngine;

    /// <summary>
    /// Concrete Strategies implement different variations 
    /// of an algorithm the context uses.
    /// 
    /// Example for a Ice damage type
    /// </summary>
    public class ConcreteStrategyA : IStrategy
    {
        public void Execute(int damage)
        {
            // Do Ice damage
            Debug.Log($"{this} => Do {damage} ice damage");
        }
    }
}
