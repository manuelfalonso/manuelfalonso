namespace SombraStudios.Patterns.Behavioural
{

    using UnityEngine;

    /// <summary>
    /// Concrete States provide their own implementations for the 
    /// state-specific methods. To avoid duplication of similar code 
    /// across multiple states, you may provide intermediate abstract 
    /// classes that encapsulate some common behavior.
    /// State objects may store a backreference to the context object. 
    /// Through this reference, the state can fetch any required info 
    /// from the context object, as well as initiate state transitions.
    /// 
    /// This example is for a Player Base behaviour
    /// </summary>
    public class ConcreteStateA : State
    {
        public ConcreteStateA(AbstractStateContext context) : base(context)
        {
        }


        public override void Start()
        {
            base.Start();
            Debug.Log($"{this} => Base Start");
        }

        public override void Handle1()
        {
            base.Handle1();
            Debug.Log($"{this} => Base Attack Dungeon");
            _context.SetState(new ConcreteStateB(_context));
        }
    }
}
