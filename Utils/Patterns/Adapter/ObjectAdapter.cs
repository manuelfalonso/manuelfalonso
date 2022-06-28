using UnityEngine;

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

public interface ILightningPhone
{
	void ConnectLightning();
	void Recharge();
}

public interface IUsbPhone
{
	void ConnectUsb();
	void Recharge();
}

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
