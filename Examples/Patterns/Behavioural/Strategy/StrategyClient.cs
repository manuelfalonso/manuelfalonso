using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// The Client creates a specific strategy object and passes it to the context. 
    /// The context exposes a setter which lets clients replace the strategy 
    /// associated with the context at runtime.
    /// 
    /// In this example we create a Ice Damage and used it with the context weapon.
    /// On runtime we change its damage to Fire.
    /// </summary>
    public class StrategyClient : MonoBehaviour
    {
        [SerializeField]
        private StrategyContext _context = default(StrategyContext);


        void Start()
        {
            UseWeapon();
        }


        private void UseWeapon()
        {
            // Do attack with Ice Damage
            var iceDamageType = new ConcreteStrategyA();
            _context.SetStrategy(iceDamageType);
            _context.TryDoAttack();

            // Use same weapon but now deal Fire Damage
            var fireDamageType = new ConcreteStrategyB();
            _context.SetStrategy(fireDamageType);
            _context.TryDoAttack();
        }
    }
}
