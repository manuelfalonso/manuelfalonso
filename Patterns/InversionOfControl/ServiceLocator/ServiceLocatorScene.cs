#if UNITY_EDITOR
#endif
using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.ServiceLocator
{
    [AddComponentMenu("Sombra Studios/ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorScene : Bootstrapper
    {
        protected override void Bootstrap()
        {
            ServiceLocator.ConfigureForScene();
        }
    }
}
