namespace SombraStudios.Shared.Examples.Patterns.Behavioural.State
{
    /// <summary>
    /// To switch the state of the context, create an instance of one 
    /// of the state classes and pass it to the context. You can do this 
    /// within the context itself, or in various states, or in the client. 
    /// Wherever this is done, the class becomes dependent on the concrete 
    /// state class that it instantiates.
    /// 
    /// In this example is a Map State Machine
    /// </summary>
    public class StateContext : AbstractStateContext
    {
        public void AttackDungeon()
        {
            _currentState.Handle1();
        }
    }
}
