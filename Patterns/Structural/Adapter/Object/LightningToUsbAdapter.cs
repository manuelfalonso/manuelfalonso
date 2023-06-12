using UnityEngine;

namespace SombraStudios.Patterns.Structural.Adapater
{
	public sealed class LightningToUsbAdapter : IUsbPhone
	{
		private readonly ILightningPhone lightningPhone;

		private bool isConnected;

		public LightningToUsbAdapter(ILightningPhone lightningPhone)
		{
			this.lightningPhone = lightningPhone;
			this.lightningPhone.ConnectLightning();
		}

		public void ConnectUsb()
		{
			this.isConnected = true;
			Debug.Log("Adapter cable connected.");
		}

		public void Recharge()
		{
			if (this.isConnected)
			{
				this.lightningPhone.Recharge();
			}
			else
			{
				Debug.Log("Connect the USB cable first.");
			}
		}
	}
}
