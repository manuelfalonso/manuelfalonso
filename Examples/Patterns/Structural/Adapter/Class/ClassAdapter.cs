using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Structural.Adapter.Class
{
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
}
