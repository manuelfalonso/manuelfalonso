using SombraStudios.Shared.Enums;
using UnityEngine;

namespace SombraStudios.Shared.Interfaces
{
    public interface ITargeteable
    {
        Team Team { get; set; }
        GameObject GameObject { get; }
    }
}
