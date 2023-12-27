using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.State
{
    /// <summary>
    /// Context stores a reference to one of the concrete state objects 
    /// and delegates to it all state-specific work. The context 
    /// communicates with the state object via the state interface. 
    /// The context exposes a setter for passing it a new state object.
    /// 
    /// It can also be called State Machine
    /// It can be an abstract class to encapsulate the State
    /// </summary>
    public abstract class AbstractStateContext : MonoBehaviour
    {
        protected State _currentState = null;

        public void SetState(State newState)
        {
            _currentState = newState;
        }
    }
}
