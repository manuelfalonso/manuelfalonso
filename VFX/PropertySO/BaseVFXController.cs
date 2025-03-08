using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    public class BaseVFXController : MonoBehaviour
    {
        protected const string PROPERTIES_TITLE = "Properties";
        protected const string DEBUG_TITLE = "Debug";
        protected const string TESTING_TITLE = "Testing";

        [Header(PROPERTIES_TITLE)]
        [SerializeField] private List<Renderer> _renderers;

        [Header(DEBUG_TITLE)]
        [SerializeField] private List<VFXPropertySO> _currentVfxProperties;

        [Header(TESTING_TITLE)]
        [SerializeField] private VFXPropertySO _vFXToApply;
        [SerializeField] private VFXPropertySO _vFXToRevert;


        public List<Renderer> Renderers => _renderers;

        #region Internal Methods
        internal virtual void AddToCurrentVFXProperties(VFXPropertySO vfx)
        {
            _currentVfxProperties.Add(vfx);
        }

        internal virtual void RemoveFromCurrentVFXProperties(VFXPropertySO vfx)
        {
            _currentVfxProperties.Remove(vfx);
        }
        #endregion

        #region Public Methods
        public virtual void ApplyVFX(VFXPropertySO vfx)
        {
            try
            {
                vfx.TryToExecute(this);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e, this);                
            }
        }

        public virtual void ApplyVFX(AnimationEvent animationEvent)
        {
            if (animationEvent.objectReferenceParameter is VFXPropertySO vfx)
            {
                ApplyVFX(vfx);
            }
            else
            {
                Debug.LogError("VFXPropertySO not found in AnimationEvent", this);
            }
        }

        public virtual void RevertVFX(VFXPropertySO vfx)
        {
            try
            {
                vfx.TryRevertVFX(this);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        public virtual void RevertVFX(AnimationEvent animationEvent)
        {
            if (animationEvent.objectReferenceParameter is VFXPropertySO vfx)
            {
                RevertVFX(vfx);
            }
            else
            {
                Debug.LogError("VFXPropertySO not found in AnimationEvent", this);
            }
        }

        public virtual void RevertAllVFX()
        {
            foreach (var vfx in _currentVfxProperties)
            {
                vfx.RevertVFX(this);
            }
        }

        public virtual void RevertVFXOnAnimationEnd()
        {
            var vfxToRevert = _currentVfxProperties.FindAll(vfx => vfx.RevertOnAnimationEnd);
            foreach (var vfx in vfxToRevert)
            {
                vfx.RevertVFX(this);
            }
        }
        #endregion

        #region Testing Methods
        public void ApplyVFXToTest()
        {
            ApplyVFX(_vFXToApply);
        }

        public void RevertVFXToTest()
        {
            RevertVFX(_vFXToRevert);
        }
        #endregion
    }
}
