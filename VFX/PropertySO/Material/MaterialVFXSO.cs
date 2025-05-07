using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    // Documentation: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Material.html
    public abstract class MaterialVFXSO<T> : VFXPropertySO
    {
        [Tooltip("The index of the renderer in the VFX controller to apply the property to.")]
        [SerializeField, Min(0)] protected int _rendererIndex;
        [Tooltip("Determines whether the property should be applied only to a specific material of the Renderer.")]
        [SerializeField] protected bool _applyOnlyToSpecificMaterial;
        [Tooltip("The index of the material in the Renderer to apply the property to.")]
        [SerializeField, Min(0)] protected int _materialIndex;

        protected static readonly Dictionary<(string, BaseVFXController), T[]> _originalProperties = new();

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            // If there are no renderers, return false
            if (vFXController.Renderers.Count == 0) return false;

            // If the renderer index is out of bounds or the renderer is null, return false
            if (_rendererIndex >= vFXController.Renderers.Count || vFXController.Renderers[_rendererIndex] == null)
                return false;

            // Get the renderer
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

        protected virtual Material[] GetMaterials(Renderer renderer)
        {
            return _applyOnlyToSpecificMaterial
                ? new[] { renderer.materials[_materialIndex] }
                : renderer.materials;
        }
    }

    public abstract class MaterialPropertyVFXSO<T> : MaterialVFXSO<Material>, IMaterialProperty<T>
    {
        [Tooltip("The name of the material shader property to be set. It will be cached OnEnable.")]
        [SerializeField] protected string _propertyName;
        [Tooltip("The value of the material shader property to be set.")]
        [SerializeField] protected T _property;

        string IMaterialProperty<T>.PropertyName => _propertyName;
        T IMaterialProperty<T>.Property => _property;

        protected int _propertyID;

        protected virtual void OnEnable()
        {
            _propertyID = Shader.PropertyToID(_propertyName);
        }

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            // Cache the materials
            var materials = GetMaterials(vFXController.Renderers[_rendererIndex]);

            foreach (var material in materials)
            {
                if (!HasProperty(_propertyID, material)) return false;
            }

            return true;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            // Cache the materials
            var materials = GetMaterials(vFXController.Renderers[_rendererIndex]);

            // Store the original value
            var originalMaterials = new Material[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalMaterials[i] = new Material(materials[i]);
            }
            _originalProperties[(Id, vFXController)] = originalMaterials;

            // Set the property
            foreach (var material in materials)
            {
                SetProperty(_propertyID, _property, material);
            }

            base.Execute(vFXController);
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            // Cache the materials
            var materials = GetMaterials(vFXController.Renderers[_rendererIndex]);

            var key = (Id, vFXController);

            if (!_originalProperties.TryGetValue(key, out var originalMaterials) || originalMaterials == null)
            {
                Debug.LogError($"No original properties found for {key}", this);
                return;
            }

            // Revert the property
            for (int i = 0; i < materials.Length; i++)
            {
                T value = GetProperty(_propertyID, originalMaterials[i]);
                SetProperty(_propertyID, value, materials[i]);
            }

            // Remove the original value
            _originalProperties.Remove(key);

            base.RevertVFX(vFXController);
        }

        public abstract bool HasProperty(int id, Material material);
        public abstract T GetProperty(int id, Material material);
        public abstract void SetProperty(int id, T value, Material material);
    }
}
