using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for disabling a specific shader keyword in materials.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewDisableKeyword", menuName = "Sombra Studios/VFX/Material/Disable Keyword")]
    public class MaterialDisableKeywordVFXSO : MaterialVFXSO<UnityEngine.Rendering.LocalKeyword>
    {
        [SerializeField] private string _keywordName;

        private UnityEngine.Rendering.LocalKeyword _localKeyword;

        /// <summary>  
        /// Determines if the VFX action can be executed.  
        /// Checks if the specified keyword is valid for the materials in the renderer.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        /// <returns>True if the action can be executed, otherwise false.</returns>  
        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (base.CanExecute(vFXController) == false) return false;

            // Cache the materials  
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);

            foreach (var material in materials)
            {
                var shader = material.shader;
                _localKeyword = new UnityEngine.Rendering.LocalKeyword(shader, _keywordName);
                // If the keyword is not valid, return false  
                if (!_localKeyword.isValid) { return false; }
            }

            return true;
        }

        /// <summary>  
        /// Executes the VFX action by disabling the specified shader keyword in the materials.  
        /// Stores the original keywords for later reversion.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        public override void Execute(BaseVFXController vFXController)
        {
            // Cache the materials  
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);

            // Store the original keywords  
            var originalKeywords = new UnityEngine.Rendering.LocalKeyword[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalKeywords[i] = new UnityEngine.Rendering.LocalKeyword(materials[i].shader, _keywordName);
            }
            _originalProperties[(Id, vFXController)] = originalKeywords;

            // Disable the keyword  
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].DisableKeyword(originalKeywords[i]);
            }

            base.Execute(vFXController);
        }

        /// <summary>  
        /// Reverts the VFX action by re-enabling the original shader keywords in the materials.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        public override void RevertVFX(BaseVFXController vFXController)
        {
            // Cache the materials  
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);
            var key = (Id, vFXController);

            // Revert the keyword  
            if (_originalProperties.TryGetValue(key, out var originalKeywords))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].EnableKeyword(originalKeywords[i]);
                }

                _originalProperties.Remove(key);
            }
            else
            {
                Debug.LogWarning($"MaterialDisableKeywordVFXSO: No original keyword found for {key}");
            }

            base.RevertVFX(vFXController);
        }
    }
}
