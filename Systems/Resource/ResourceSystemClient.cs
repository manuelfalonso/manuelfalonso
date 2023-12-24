using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{

    /// <summary>
    /// Example client script testing a Player Health
    /// </summary>
    public class ResourceSystemClient : MonoBehaviour
    {
        [SerializeField] private ResourceSystem _playerHealthSystem = null;


        IEnumerator Start()
        {
            // Life Resource example
            _playerHealthSystem.AmountEmptied.AddListener(PlayerHealthSystem_OnEmptyResource);
            _playerHealthSystem.AmountChanged.AddListener(PlayerHealthSystem_OnResourceChanged);
            _playerHealthSystem.AmountRestored.AddListener(PlayerHealthSystem_OnRestoreResource);
            _playerHealthSystem.AmountMaxed.AddListener(PlayerHealthSystem_OnMaxResource);
            _playerHealthSystem.AmountLow.AddListener(PlayerHealthSystem_OnLowResource);

            // Get data
            Log($"* Player Initial life: {_playerHealthSystem.Amount}", this);
            Log($"* Player Max life: {_playerHealthSystem.MaxAmount}", this);
            Log($"* Player has {_playerHealthSystem.ResourcePercent * 100f}% of life", this);

            // Methods
            Log($"* Damage Player", this);
            _playerHealthSystem.DecreaseAmountWithResult(80f);
            Log($"* Heal Player to max Health", this);
            _playerHealthSystem.IncreaseAmountWithResult(9999f);
            Log($"* Tree fall and killed the Player", this);
            _playerHealthSystem.ClearAmountWithResult();
            Log($"* Revive Player by half of this life", this);
            _playerHealthSystem.RestoreAmountWithResult(0.5f);
            Log($"* Player health now is immutable", this);
            _playerHealthSystem.Immutable = true;
            Log($"* Tring to damage the Player", this);
            _playerHealthSystem.DecreaseAmountWithResult(10f);
            Log($"* Player health now is not immutable", this);
            _playerHealthSystem.Immutable = false;
            Log($"* Killed the Player, AGAIN!", this);
            _playerHealthSystem.ClearAmountWithResult();
            Log($"* But now reset it to initial amount", this);
            _playerHealthSystem.ResetAmountWithResult();

            yield break;
        }


        private void PlayerHealthSystem_OnLowResource(float healthAmount)
        {
            Log($"Player OnLowResource", this);
        }

        private void PlayerHealthSystem_OnMaxResource(float healthAmount)
        {
            Log($"Player OnMaxResource", this);
        }

        private void PlayerHealthSystem_OnRestoreResource(float healthAmount)
        {
            Log($"Player OnRestoreResource", this);
        }

        private void PlayerHealthSystem_OnResourceChanged(float healthAmount)
        {
            Log($"Player OnResourceChanged: {_playerHealthSystem.Amount}", this);
        }

        private void PlayerHealthSystem_OnEmptyResource(float healthAmount)
        {
            Log($"Player OnEmptyResource", this);
        }


        public void Log(object message, Object sender = null)
        {
            if (sender != null)
                Debug.Log($"{this} => {message}", sender);
            else
                Debug.Log($"{this} => {message}");
        }
    }
}
