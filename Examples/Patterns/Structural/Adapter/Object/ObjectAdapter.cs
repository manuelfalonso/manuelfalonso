using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Structural.Adapter.Object
{
	/// <summary>
	/// Converts the interface of one object so that another object can understand it.
	/// This implementation uses the object composition principle
	/// </summary>
	public class ObjectAdapter : MonoBehaviour
	{
		private void Start()
		{
			ILightningPhone applePhone = new ApplePhone();
			IUsbPhone adapterCable = new LightningToUsbAdapter(applePhone);

			adapterCable.ConnectUsb();
			adapterCable.Recharge();
		}
	}
}
