using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.DependencyInjection.Example
{
    /// <summary>
    /// Example class
    /// </summary>
    public class ExampleDependencyMonobehaviour : MonoBehaviour
    {
        public void DoSomethingComplex()
        {
            Debug.Log($"Something complex just happened: {gameObject.GetInstanceID()}");
        }
    }
}
