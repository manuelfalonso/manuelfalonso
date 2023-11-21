namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// The Strategy interface is common to all concrete strategies. 
    /// It declares a method the context uses to execute a strategy.
    /// 
    /// This example is for executing a Weapon damage type
    /// </summary>
    public interface IStrategy
    {
        public void Execute(int damage);
    }
}
