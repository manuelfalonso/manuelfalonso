using SombraStudios.Shared.Systems.Resource;
using System;

namespace SombraStudios.Shared.Interfaces
{
    public interface IDamageable
    {
        Action<DamageData> Damaged { get; set; }

        bool TryTakeDamage(DamageData data);
    }
}
