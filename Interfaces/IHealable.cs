using SombraStudios.Shared.Systems.Resource;
using System;

namespace SombraStudios.Shared.Interfaces
{
    public interface IHealable
    {
        Action<HealData> Healed { get; set; }

        bool TryHeal(HealData data);
    }
}
