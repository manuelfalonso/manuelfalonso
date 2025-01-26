using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// The Context maintains a reference to one of the concrete strategies 
    /// and communicates with this object only via the strategy interface.
    /// 
    /// This example is for a weapon. It can be a base class for other
    /// types of weapons.
    /// Also, you can combine different Damage types as shown below.
    /// It can also not be a MonoBehaviour and initialize its data
    /// on the constructor in a Client class.
    /// </summary>
    public class DamageStrategyContext : MonoBehaviour
    {
        [SerializeField] private int _damage = 0;

        private IDamageStrategy _damageStrategy;


        /// <summary>
        /// Do Strategy
        /// </summary>
        public void TryDoAttack()
        {
            _damageStrategy?.Execute(_damage);
        }

        /// <summary>
        /// Switch strategy on runtime
        /// </summary>
        /// <param name="damageStrategy"></param>
        public void SetStrategy(IDamageStrategy damageStrategy)
        {
            if (damageStrategy != null)
                _damageStrategy = damageStrategy;
        }


        // Combine Strategies
        [SerializeField] private List<IDamageStrategy> _strategyList = new();

        public void TryDoCombineAttack()
        {
            foreach (IDamageStrategy strategy in _strategyList)
            {
                strategy?.Execute(_damage);
            }
        }

        public void AddDamageType(IDamageStrategy newDamageStrategy)
        {
            _strategyList.Add(newDamageStrategy);
        }
    }
}