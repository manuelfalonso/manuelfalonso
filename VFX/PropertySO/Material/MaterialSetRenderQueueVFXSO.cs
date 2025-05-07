using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting the render queue of materials.  
    /// Allows customization of the render order of objects in the scene.  
    /// Note: When Unity runs in batch mode, it does not load Scriptable Render Pipelines (SRPs) until  
    /// the first time something renders. Loading an SRP modifies the sub-shader selected for a given  
    /// material, which can lead to the value this function returns being different than expected.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetRenderQueue", menuName = "Sombra Studios/VFX/Material/Set Render Queue")]
    public class MaterialSetRenderQueueVFXSO : MaterialVFXSO<int>
    {
        [Tooltip("The render queue to set. -1 is the shader default render queue. The range value is from " +
            "0 to 5000.")]
        [SerializeField, Range(-1, 5000)] private int _renderQueue = -1;

        /// <summary>  
        /// Executes the VFX action by setting the render queue of the materials.  
        /// Stores the original render queue values for later reversion.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        public override void Execute(BaseVFXController vFXController)
        {
            // Cache the materials  
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);

            // Store the original render queue  
            var originalRenderQueue = new int[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalRenderQueue[i] = materials[i].renderQueue;
            }
            _originalProperties[(Id, vFXController)] = originalRenderQueue;

            // Set the render queue  
            foreach (var material in materials)
            {
                material.renderQueue = _renderQueue;
            }

            base.Execute(vFXController);
        }

        /// <summary>  
        /// Reverts the VFX action by restoring the original render queue values of the materials.  
        /// </summary>  
        /// <param name="vFXController">The VFX controller managing the renderer and materials.</param>  
        public override void RevertVFX(BaseVFXController vFXController)
        {
            // Cache the materials  
            var renderer = vFXController.Renderers[_rendererIndex];
            var materials = GetMaterials(renderer);
            var key = (Id, vFXController);

            // Revert the render queue  
            if (_originalProperties.TryGetValue(key, out var originalRenderQueue))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].renderQueue = originalRenderQueue[i];
                }

                _originalProperties.Remove(key);
            }
            else
            {
                Debug.LogWarning($"MaterialSetRenderQueueVFXSO: No original render queue found for {key}");
            }

            base.RevertVFX(vFXController);
        }
    }
}
