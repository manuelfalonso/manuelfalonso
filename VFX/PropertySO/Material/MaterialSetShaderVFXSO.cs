using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a shader on materials.  
    /// Allows changing the shader of materials at runtime and reverting to the original shader.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetShader", menuName = "Sombra Studios/VFX/Material/Set Shader")]
    public class MaterialSetShaderVFXSO : MaterialVFXSO<Shader>
    {
        // Is the name you can see in the shader popup of any material, for example "Standard",
        // "Unlit/Texture", "Legacy Shaders/Diffuse" etc.
        // Note that a shader might be not included into the player build if nothing references it. In that case,
        // Shader.Find will work only in the Editor, and will result in the pink error shader in a build.
        // Because of that, it is advisable to use shader references instead of finding them by name.
        // To make sure a shader is included into the game build, do either of: 1) reference it from
        // some of the materials used in your Scene, 2) add it under "Always Included Shaders" list
        // in ProjectSettings/Graphics or 3) put shader or something that references it (e.g. a Material)
        // into a "Resources" folder.
        /// <summary>  
        /// The name of the shader to set.  
        /// Note: Ensure the shader is included in the build to avoid runtime issues.  
        /// </summary>
        [Tooltip("The name of the shader to set. Note: Ensure the shader is included in " +
            "the build to avoid runtime issues.")]
        [SerializeField] private string _shaderName;

        private Shader _shader;

        protected virtual void OnEnable()
        {
            _shader = Shader.Find(_shaderName);
            if (_shader == null)
            {
                Debug.LogWarning($"Shader '{_shaderName}' not found. Ensure it is included in the build.", this);
            }
        }

        /// <summary>  
        /// Determines if the VFX action can be executed.  
        /// Checks if the shader is valid and the base conditions are met.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        /// <returns>True if the action can be executed, otherwise false.</returns>  
        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (base.CanExecute(vFXController) == false) return false;

            // If the shader is null, return false
            if (_shader == null) return false;

            return true;
        }

        /// <summary>  
        /// Executes the VFX action by setting the specified shader on the materials.  
        /// Stores the original shaders for later reversion.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        public override void Execute(BaseVFXController vFXController)
        {
            // Cache the materials
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);

            // Store the original shader
            var originalShaders = new Shader[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalShaders[i] = materials[i].shader;
            }
            _originalProperties[(Id, vFXController)] = originalShaders;

            // Set the shader
            foreach (var material in materials)
            {
                material.shader = _shader;
            }

            base.Execute(vFXController);
        }

        /// <summary>  
        /// Reverts the VFX action by restoring the original shaders on the materials.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param> 
        public override void RevertVFX(BaseVFXController vFXController)
        {
            // Cache the materials
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);
            var key = (Id, vFXController);

            // Revert the shader
            if (_originalProperties.TryGetValue(key, out var originalShaders))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].shader = originalShaders[i];
                }

                _originalProperties.Remove(key);
            }
            else
            {
                Debug.LogWarning($"MaterialSetShaderVFXSO: No original shader found for {key}", this);
            }

            base.RevertVFX(vFXController);
        }
    }
}
