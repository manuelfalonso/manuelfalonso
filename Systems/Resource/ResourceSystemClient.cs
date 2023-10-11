using System.Collections;
using UnityEngine;

namespace SombraStudios.Systems.Resource
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
            _playerHealthSystem.DecreaseAmount(80f);
            Log($"* Heal Player to max Health", this);
            _playerHealthSystem.IncreaseAmount(9999f);
            Log($"* Tree fall and killed the Player", this);
            _playerHealthSystem.ClearAmount();
            Log($"* Revive Player by half of this life", this);
            _playerHealthSystem.RestoreAmount(0.5f);
            Log($"* Player health now is immutable", this);
            _playerHealthSystem.Immutable = true;
            Log($"* Tring to damage the Player", this);
            _playerHealthSystem.DecreaseAmount(10f);
            Log($"* Player health now is not immutable", this);
            _playerHealthSystem.Immutable = false;
            Log($"* Killed the Player, AGAIN!", this);
            _playerHealthSystem.ClearAmount();
            Log($"* But now reset it to initial amount", this);
            _playerHealthSystem.ResetAmount();

            yield break;
        }


        private void PlayerHealthSystem_OnLowResource()
        {
            Log($"Player OnLowResource", this);
        }

        private void PlayerHealthSystem_OnMaxResource()
        {
            Log($"Player OnMaxResource", this);
        }

        private void PlayerHealthSystem_OnRestoreResource()
        {
            Log($"Player OnRestoreResource", this);
        }

        private void PlayerHealthSystem_OnResourceChanged()
        {
            Log($"Player OnResourceChanged: {_playerHealthSystem.Amount}", this);
        }

        private void PlayerHealthSystem_OnEmptyResource()
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
