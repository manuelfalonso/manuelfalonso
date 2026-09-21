#if NETCODE_GAMEOBJECTS
using Unity.Netcode;
using UnityEngine;

namespace SombraStudios.Shared.Networking.Netcode
{

	/// <summary>
	/// Example Class of a Network Variable of type Vector3 where the server 
	/// directly modify the Variable. In the case of a client it ask through 
	/// Server Rpc to modify the Variable.
	/// </summary>
	public class NetworkVariableExample : NetworkBehaviour
	{
		public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

		[Tooltip("How quickly the transform catches up to the replicated position.")]
		[SerializeField] private float _moveSpeed = 5f;
			
		public override void OnNetworkSpawn()
		{
			Move();
		}

		public void Move()
		{
			if (NetworkManager.Singleton.IsServer)
			{
				Position.Value = GetRandomPositionOnPlane();
			}
			else
			{
				SubmitPositionRequestServerRpc();
			}
		}

		[ServerRpc]
		void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
		{
			Position.Value = GetRandomPositionOnPlane();
		}

		static Vector3 GetRandomPositionOnPlane()
		{
			return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
		}

		void Update()
		{
			transform.localPosition = Vector3.Lerp(
				transform.localPosition, Position.Value, _moveSpeed * Time.deltaTime);
		}
	}
}
#endif