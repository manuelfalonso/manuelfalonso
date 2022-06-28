using UnityEngine;

/// <summary>
/// Converts the interface of one object so that another object can understand it.
/// This implementation uses inheritance
/// </summary>
public class ClassAdapter : MonoBehaviour
{
    private void Start()
    {
        Adaptee adaptee = new Adaptee("-34.123456", "-58.123456");
        TargetDataClass target = new Adapter(adaptee);

        Debug.Log("Adaptee class is incompatible with the client.");
        Debug.Log("But with adapter client can call it's method.");

        Debug.Log(target.GetValue());
    }
}

// The Target defines the domain-specific class used by the client code.
public class TargetDataClass
{
    // Format 40.6976637,-74.119764
    string latitudeLongitude;

    public virtual string GetValue()
    {
        return latitudeLongitude;
    }
}

// The Adaptee contains some useful behavior, but its data is
// incompatible with the existing client code. The Adaptee needs some
// adaptation before the client code can use it.
class Adaptee
{
    public string latitude;
    public string longitude;

    public Adaptee(string lat, string lon)
    {
        latitude = lat;
        longitude = lon;
    }
}

// The Adapter makes the Adaptee's data compatible with the Target's
// class.
class Adapter : TargetDataClass
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee) => _adaptee = adaptee;

    public override string GetValue() => 
        _adaptee.latitude + "," + _adaptee.longitude;
}
