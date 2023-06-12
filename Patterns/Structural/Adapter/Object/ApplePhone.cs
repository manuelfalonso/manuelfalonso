using UnityEngine;

namespace SombraStudios.Patterns.Structural.Adapater
{
	public sealed class ApplePhone : ILightningPhone
	{
		private bool isConnected;

		public void ConnectLightning()
		{
			this.isConnected = true;
			Debug.Log("Apple phone connected.");
		}

		public void Recharge()
		{
			if (this.isConnected)
			{
				Debug.Log("Apple phone recharging.");
			}
			else
			{
				Debug.Log("Connect the Lightning cable first.");
			}
		}
	}
}
