#if UNITY_XR_INTERACTION_TOOLKIT
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#endif

namespace SombraStudios.Shared.XR
{
    /// <summary>
    /// This component makes sure that the attached <c>Interactor</c> always have an interactable selected.
    /// This is accomplished by forcing the <c>Interactor</c> to select a new <c>Interactable Prefab</c> instance whenever
    /// it loses the current selected interactable.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(XRBaseInteractor))]
    public class XRInfiniteInteractable : MonoBehaviour
    {
        [Tooltip("Whether infinite spawning is active.")]
        [SerializeField] private bool _active = true;

        [Tooltip("If true then during Awake the Interactor \"Starting Selected Interactable\" will be overriden by an " +
                 "instance of the \"Interactable Prefab\".")]
        [SerializeField] private bool _overrideStartingSelectedInteractable;


        [Tooltip("The Prefab or GameObject to be instantiated and selected.")]
        [SerializeField] private XRBaseInteractable _interactablePrefab;

        XRBaseInteractor _interactor;

        /// <summary>
        /// Whether infinite spawning is enabled.
        /// </summary>
        public bool Active
        {
            get => _active;

            set
            {
                _active = value;
                if (enabled && value && !_interactor.hasSelection)
                    InstantiateAndSelectInteractable();
            }
        }


        private void Awake()
        {
            _interactor = GetComponent<XRBaseInteractor>();

            if (_overrideStartingSelectedInteractable) { OverrideStartingSelectedInteractable(); }                
        }

        private void OnEnable()
        {
            if (_interactablePrefab == null)
            {
                Debug.LogWarning("No interactable prefab set - nothing to spawn!");
                enabled = false;
                return;
            }
            _interactor.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            _interactor.selectExited.RemoveListener(OnSelectExited);
        }


        private void OnSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            if (selectExitEventArgs.isCanceled || !Active) { return; }                

            InstantiateAndSelectInteractable();
        }

        private XRBaseInteractable InstantiateInteractable()
        {
            var socketTransform = _interactor.transform;
            return Instantiate(_interactablePrefab, socketTransform.position, socketTransform.rotation);
        }

        private void OverrideStartingSelectedInteractable()
        {
            _interactor.startingSelectedInteractable = InstantiateInteractable();
        }

        private void InstantiateAndSelectInteractable()
        {
            if (!gameObject.activeInHierarchy || _interactor.interactionManager == null) { return; }                

            _interactor.interactionManager.SelectEnter((IXRSelectInteractor)_interactor, InstantiateInteractable());
        }
    }
}
#endif