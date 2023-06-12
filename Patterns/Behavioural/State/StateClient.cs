namespace SombraStudios.Patterns.Behavioural.State
{
    using UnityEngine;

    /// <summary>
    /// The client initiliaze the State Machine Behaviour
    /// 
    /// In this example the Map is intialize with the Player Base behaviour
    /// Then during gameplay it switch to the Dungeon Behaviour
    /// </summary>
    public class StateClient : MonoBehaviour
    {
        [SerializeField]
        private StateContext _stateContext = default;


        private void Start()
        {
            StateMachineInit();
            Gameplay();
        }


        private void StateMachineInit()
        {
            if (_stateContext == null)
                return;

            State initialState = new ConcreteStateA(_stateContext);
            _stateContext.SetState(initialState);
        }

        private void Gameplay()
        {
            _stateContext.AttackDungeon();
        }
    }
}
