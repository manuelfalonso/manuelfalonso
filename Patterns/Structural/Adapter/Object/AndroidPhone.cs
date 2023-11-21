using UnityEngine;

namespace SombraStudios.Shared.Patterns.Structural.Adapater
{
	public sealed class AndroidPhone : IUsbPhone
	{
		private bool isConnected;

		public void ConnectUsb()
		{
			this.isConnected = true;
			Debug.Log("Android phone connected.");
		}

		public void Recharge()
		{
			if (this.isConnected)
			{
				Debug.Log("Android phone recharging.");
			}
			else
			{
				Debug.Log("Connect the USB cable first.");
			}
		}
	}
}
