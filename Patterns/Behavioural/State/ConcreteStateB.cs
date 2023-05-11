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
    /// This example is for a Dungeon behaviour
    /// </summary>
    public class ConcreteStateB : State
    {
        public ConcreteStateB(AbstractStateContext context) : base(context)
        {
        }


        public override void Start()
        {
            base.Start();
            Debug.Log($"{this} => Dungeon Start");
            Handle2();
        }

        public override void Handle2()
        {
            base.Handle2();
            Debug.Log($"{this} => Dungeon Player Death");
        }
    }
}
