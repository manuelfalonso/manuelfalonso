namespace SombraStudios.Patterns.Behavioural
{

    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The Context maintains a reference to one of the concrete strategies 
    /// and communicates with this object only via the strategy interface.
    /// 
    /// This example is for a weapon. It can be a base class for other
    /// types of weapons.
    /// Also you can combine different Damage types as shown below.
    /// It can also not be a MonoBehaviour and initialize its data
    /// on the constructor in a Client class.
    /// </summary>
    public class StrategyContext : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 0;

        private IStrategy _strategy;


        /// <summary>
        /// Do Strategy
        /// </summary>
        public void TryDoAttack()
        {
            _strategy?.Execute(_damage);
        }

        /// <summary>
        /// Switch strategy on runtime
        /// </summary>
        /// <param name="strategy"></param>
        public void SetStrategy(IStrategy strategy)
        {
            if (strategy != null)
                _strategy = strategy;
        }


        // Combine Strategies
        [SerializeField]
        private List<IStrategy> _strategyList = new List<IStrategy>();

        public void TryDoCombineAttack()
        {
            foreach (IStrategy strategy in _strategyList)
            {
                strategy?.Execute(_damage);
            }
        }

        public void AddDamageType(IStrategy newStrategy)
        {
            _strategyList.Add(newStrategy);
        }
    }
}
