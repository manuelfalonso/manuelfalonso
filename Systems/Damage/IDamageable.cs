using System;

namespace SombraStudios.Shared.Systems.Damage
{
    public interface IDamageable
    {
        Action<DamageData> Damaged { get; set; }

        bool TryTakeDamage(DamageData data);
    }
}
