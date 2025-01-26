using SombraStudios.Shared.Patterns.Behavioural.Strategy;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// The Strategy interface is common to all concrete strategies. 
    /// It declares a method the context uses to execute a strategy.
    /// 
    /// This example is for executing a Weapon damage type
    /// </summary>
    public interface IDamageStrategy : IStrategy<int> { }
}
