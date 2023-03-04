using UnityEngine;

namespace DependencyInjection.Example
{

    /// <summary>
    /// In this case scneario, the nested class is in a Prefab
    /// as you can see on ExampleDependenciesContext line 35
    /// </summary>
    public class ExampleDependencyNested : MonoBehaviour
    {
        public void DoSomethingSimple()
        {
            Debug.Log($"Something simple from a nested dependency: {gameObject.GetInstanceID()}");
        }
    }
}
