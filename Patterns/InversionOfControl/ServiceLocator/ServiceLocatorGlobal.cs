#if UNITY_EDITOR
#endif
using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.ServiceLocator
{
    [AddComponentMenu("Sombra Studios/ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobal : Bootstrapper
    {
        [SerializeField] private bool _dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            ServiceLocator.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }
}
