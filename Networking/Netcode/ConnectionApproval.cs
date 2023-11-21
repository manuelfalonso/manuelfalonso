#if REQUIRES_EXTERNAL_PACKAGE
using Unity.Netcode;
using UnityEngine;

namespace SombraStudios.Shared.Networking
{
    /// <summary>
    /// Approval check using a string room password.
    /// </summary>
    public class ConnectionApproval : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionToSpawnAt;
        [SerializeField] private Quaternion _rotationToSpawnWith;

        [SerializeField] private string _roomPassword = "1234";
        [SerializeField] private string _inputRoomPassword = "5678";

        private void SetupHost()
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
            NetworkManager.Singleton.StartHost();
        }

        private void SetupClient()
        {
            NetworkManager.Singleton.NetworkConfig.ConnectionData = 
                System.Text.Encoding.ASCII.GetBytes(_inputRoomPassword);
            NetworkManager.Singleton.StartClient();
        }

        private void ApprovalCheck(
            byte[] connectionData, 
            ulong clientId, 
            NetworkManager.ConnectionApprovedDelegate callback)
        {
            //Your logic here
            //bool approve = true;
            bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == _roomPassword;
            bool createPlayerObject = true;

            // If approve is true, the connection gets added. If it's false. 
            // The client gets disconnected
            callback(
                createPlayerObject,
                null, // The prefab hash. Use null to use the default player prefab
                approve,
                _positionToSpawnAt,
                _rotationToSpawnWith);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 250, 300));

            GUILayout.Label("Test Setup Connection Approval");
            GUILayout.Label("Room Password: " + _roomPassword);
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                if (GUILayout.Button("Start Host")) SetupHost();
                if (GUILayout.Button("Start Client")) SetupClient();
            }
            GUILayout.Label("Input Room Password");
            _inputRoomPassword = GUILayout.TextField(_inputRoomPassword, 4);

            GUILayout.EndArea();
        }
    }
}
#endif