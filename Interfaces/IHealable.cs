using SombraStudios.Shared.Systems.Resource.Data;
using System;

namespace SombraStudios.Shared.Interfaces
{
    public interface IHealable
    {
        Action<HealData> Healed { get; set; }

        bool TryHeal(HealData data);
    }
}
