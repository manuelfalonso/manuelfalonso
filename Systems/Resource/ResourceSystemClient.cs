using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Example client script testing a Player Health
    /// </summary>
    public class ResourceSystemClient : FloatResourceSystem
    {
        IEnumerator Start()
        {
            // Life Resource example
            AmountEmptied.AddListener(PlayerHealthSystem_OnEmptyResource);
            AmountChanged.AddListener(PlayerHealthSystem_OnResourceChanged);
            AmountRestored.AddListener(PlayerHealthSystem_OnRestoreResource);
            AmountMaxed.AddListener(PlayerHealthSystem_OnMaxResource);

            // Get data
            Utility.Loggers.Logger.Log($"* Player Initial life: {Amount}", this);
            Utility.Loggers.Logger.Log($"* Player Max life: {MaxAmount}", this);
            Utility.Loggers.Logger.Log($"* Player has {ResourcePercent * 100f}% of life", this);

            // Methods
            Utility.Loggers.Logger.Log($"* Damage Player", this);
            TryDecreaseAmount(80f, out _);
            Utility.Loggers.Logger.Log($"* Heal Player to max Health", this);
            TryIncreaseAmount(9999f, out _);
            Utility.Loggers.Logger.Log($"* Tree fall and killed the Player", this);
            TryClearAmount(out _);
            Utility.Loggers.Logger.Log($"* Revive Player by half of this life", this);
            TryRestoreAmount(out _, 0.5f);
            Utility.Loggers.Logger.Log($"* Player health now is immutable", this);
            IsImmutable = true;
            Utility.Loggers.Logger.Log($"* Tring to damage the Player", this);
            TryDecreaseAmount(10f, out _);
            Utility.Loggers.Logger.Log($"* Player health now is not immutable", this);
            IsImmutable = false;
            Utility.Loggers.Logger.Log($"* Killed the Player, AGAIN!", this);
            TryClearAmount(out _);
            Utility.Loggers.Logger.Log($"* But now reset it to initial amount", this);
            TryResetAmount(out _);

            yield break;
        }


        private void PlayerHealthSystem_OnMaxResource(ResourceSystemData data)
        {
            Utility.Loggers.Logger.Log($"Player OnMaxResource", this);
        }

        private void PlayerHealthSystem_OnRestoreResource(ResourceSystemData data)
        {
            Utility.Loggers.Logger.Log($"Player OnRestoreResource", this);
        }

        private void PlayerHealthSystem_OnResourceChanged(ResourceSystemData data)
        {
            Utility.Loggers.Logger.Log($"Player OnResourceChanged: {Amount}", this);
        }

        private void PlayerHealthSystem_OnEmptyResource(ResourceSystemData data)
        {
            Utility.Loggers.Logger.Log($"Player OnEmptyResource", this);
        }
    }
}
