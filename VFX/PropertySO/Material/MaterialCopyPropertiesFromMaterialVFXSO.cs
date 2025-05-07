using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// This SO copies property values (both serialized and set at runtime), as well as shader keywords, 
    /// render queue and global illumination flags from the other material. Material's shader is not changed.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCopyPropertiesFromMaterial", menuName = "Sombra Studios/VFX/Material/Copy Properties From Material")]
    public class MaterialCopyPropertiesFromMaterialVFXSO : MaterialVFXSO<Material>
    {
        [SerializeField] private Material _sourceMaterial;

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (base.CanExecute(vFXController) == false) return false;

            // If the source material is null, return false
            if (_sourceMaterial == null) return false;

            return true;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            // Cache the materials
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);

            // Store the original materials
            var originalMaterials = new Material[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalMaterials[i] = new Material(materials[i]);
            }
            _originalProperties[(Id, vFXController)] = originalMaterials;

            // Copy the properties
            foreach (var material in materials)
            {
                material.CopyPropertiesFromMaterial(_sourceMaterial);
            }

            base.Execute(vFXController);
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            // Cache the materials
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);
            var key = (Id, vFXController);

            // Revert the properties
            if (_originalProperties.TryGetValue(key, out var originalMaterials))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].CopyPropertiesFromMaterial(originalMaterials[i]);
                }

                _originalProperties.Remove(key);
            }
            else
            {
                Debug.LogWarning($"MaterialCopyPropertiesFromMaterialVFXSO: No original properties found for {key}");
            }

            base.RevertVFX(vFXController);
        }
    }
}
