using System;

namespace SombraStudios.Shared.Systems.Heal
{
    public interface IHealable
    {
        Action<HealData> Healed { get; set; }

        bool TryHeal(HealData data);
    }
}
