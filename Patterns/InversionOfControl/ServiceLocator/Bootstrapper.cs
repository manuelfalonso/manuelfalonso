using SombraStudios.Shared.Extensions;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.ServiceLocator
{
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        internal ServiceLocator ServiceLocator => _serviceLocator.OrNull() ?? (_serviceLocator = GetComponent<ServiceLocator>());

        private ServiceLocator _serviceLocator;
        private bool _isInitialized = false;

        protected virtual void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }
}
