using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Abstract base class for VFX properties that use MaterialPropertyBlock.
    /// Note that this is not compatible with SRP Batcher. Using this in the Universal Render Pipeline (URP),
    /// High Definition Render Pipeline (HDRP) or a custom render pipeline based on the Scriptable Render
    /// Pipeline (SRP) will likely result in a drop in performance.
    /// Documentation: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MaterialPropertyBlock.html
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    public abstract class MaterialPropertyBlockVFXSO<T> : VFXPropertySO
    {
        [Tooltip("The index of the renderer in the VFX controller to apply the property to.")]
        [SerializeField, Min(0)] protected int _rendererIndex;
        [Tooltip("Determines whether the property should be applied only to a specific material of the Renderer.")]
        [SerializeField] protected bool _applyOnlyToSpecificMaterial;
        [Tooltip("The index of the material in the Renderer to apply the property to.")]
        [SerializeField, Min(0)] protected int _materialIndex;

        protected static readonly Dictionary<(string, BaseVFXController), T> _originalProperties = new();

        protected BaseVFXController _vFXController;

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            // If there are no renderers, return false
            if (vFXController.Renderers.Count == 0) return false;

            // If the renderer index is out of bounds or the renderer is null, return false
            if (_rendererIndex >= vFXController.Renderers.Count || vFXController.Renderers[_rendererIndex] == null)
                return false;

            var renderer = vFXController.Renderers[_rendererIndex];
            if (_applyOnlyToSpecificMaterial)
            {
                // If the material index is out of bounds or the material is null, return false
                if (_materialIndex >= renderer.materials.Length || renderer.materials[_materialIndex] == null)
                    return false;
            }
            else
            {
                // If the renderer has no materials, return false
                if (renderer.materials.Length == 0) return false;
            }

            return true;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            var renderer = vFXController.Renderers[_rendererIndex];
            _vFXController = vFXController;

            // Get the current material property block
            var mpb = new MaterialPropertyBlock();
            if (_applyOnlyToSpecificMaterial)
            {
                renderer.GetPropertyBlock(mpb, _materialIndex);
            }
            else
            {
                renderer.GetPropertyBlock(mpb);
            }

            // Store the original value before applying the new one
            StoreOriginalValue(mpb);

            // Apply the new value
            ApplyNewValue(mpb);
            renderer.SetPropertyBlock(mpb);

            base.Execute(vFXController);
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            var renderer = vFXController.Renderers[_rendererIndex];

            var key = (Id, vFXController);
            if (_originalProperties.TryGetValue(key, out T originalValue))
            {
                // Get the current material property block
                var mpb = new MaterialPropertyBlock();
                if (_applyOnlyToSpecificMaterial)
                {
                    renderer.GetPropertyBlock(mpb, _materialIndex);
                }
                else
                {
                    renderer.GetPropertyBlock(mpb);
                }

                // Restore the original property
                RestoreOriginalValue(mpb, originalValue);
                renderer.SetPropertyBlock(mpb);

                // Remove the stored value to prevent memory leaks
                _originalProperties.Remove(key);
            }

            base.RevertVFX(vFXController);
        }

        /// <summary>
        /// Stores the original value of the property in the material property block.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        protected abstract void StoreOriginalValue(MaterialPropertyBlock mpb);

        /// <summary>
        /// Applies the new value of the property to the material property block.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        protected abstract void ApplyNewValue(MaterialPropertyBlock mpb);

        /// <summary>
        /// Restores the original value of the property in the material property block.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        /// <param name="originalValue">The original value of the property.</param>
        protected abstract void RestoreOriginalValue(MaterialPropertyBlock mpb, T originalValue);
    }

    /// <summary>
    /// Abstract base class for VFX properties that use MaterialPropertyBlock and have a specific property name and value.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    public abstract class MaterialPropertyBlockPropertyVFXSO<T> : MaterialPropertyBlockVFXSO<T>, IMPBProperty<T>
    {
        [Tooltip("The name of the material shader property to be set. It will be cached OnEnable.")]
        [SerializeField] protected string _propertyName;
        [Tooltip("The value of the material shader property to be set.")]
        [SerializeField] protected T _property;

        protected int _propertyID;

        string IMPBProperty<T>.PropertyName { get => _propertyName; set => _propertyName = value; }
        T IMPBProperty<T>.Property { get => _property; set => _property = value; }

        private void OnEnable()
        {
            _propertyID = Shader.PropertyToID(_propertyName);
        }

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            var renderer = vFXController.Renderers[_rendererIndex];
            if (_applyOnlyToSpecificMaterial)
            {
                if (!renderer.sharedMaterials[_materialIndex].HasProperty(_propertyID)) return false;
            }
            else
            {
                if (!renderer.sharedMaterial.HasProperty(_propertyID)) return false;
            }

            return true;
        }

        protected override void StoreOriginalValue(MaterialPropertyBlock mpb)
        {
            if (HasProperty(mpb))
            {
                var key = (Id, _vFXController);
                _originalProperties[key] = GetProperty(mpb);
            }
        }

        protected override void ApplyNewValue(MaterialPropertyBlock mpb)
        {
            SetProperty(mpb, _property);
        }

        protected override void RestoreOriginalValue(MaterialPropertyBlock mpb, T originalValue)
        {
            SetProperty(mpb, originalValue);
        }


        public abstract bool HasProperty(MaterialPropertyBlock mpb);

        public abstract T GetProperty(MaterialPropertyBlock mpb);

        public abstract void SetProperty(MaterialPropertyBlock mpb, T value);
    }
}
