namespace SombraStudios.Patterns.Behavioural
{

    using UnityEngine;

    /// <summary>
    /// The State interface declares the state-specific methods. 
    /// These methods should make sense for all concrete states 
    /// because you don’t want some of your states to have useless 
    /// methods that will never be called.
    /// 
    /// For this example the state will handle the start,
    /// attack and death methods.
    /// 
    /// The methods could be either abstract or virtual depending
    /// if a base behaviour is needed.
    /// </summary>
    public abstract class State
    {
        protected AbstractStateContext _context;


        protected State(AbstractStateContext context)
        {
            _context = context;
            Start();
        }


        public virtual void Start()
        {
            Debug.Log($"{this} => Start");
        }

        public virtual void Handle1()
        {
            Debug.Log($"{this} => Attack!");
        }

        public virtual void Handle2()
        {
            Debug.Log($"{this} => Death :(");
        }
    }
}
