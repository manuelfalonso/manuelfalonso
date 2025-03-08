using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// VFX to set a blend shape weight of a SkinnedMeshRenderer.
    /// This VFX only works if the blend shape is not being updated in ANY animation of any layer of the animator.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSetBlendShape", menuName = "Sombra Studios/VFX/Blend Shape/Set Blend Shape")]
    public class SetBlendShapeVFXSO : VFXPropertySO, IBlendShapeProperty
    {
        private const float MIN_WEIGHT = 0f;
        private const float MAX_WEIGHT = 100f;

        [Header(PROPERTIES_TITLE)]
        [SerializeField] private int _rendererIndex;
        [SerializeField] private string _blendShapeName;
        [SerializeField] private float _weight;

        public string BlendShapeName => _blendShapeName;
        public float Weight => _weight;

        private static readonly Dictionary<(string, BaseVFXController), float> _originalProperties = new();

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            if (vFXController.Renderers.Count == 0) return false;

            if (_rendererIndex >= vFXController.Renderers.Count || vFXController.Renderers[_rendererIndex] == null)
                return false;

            if (vFXController.Renderers[_rendererIndex] is not SkinnedMeshRenderer skinnedMeshRenderer) return false;

            var blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
            return blendShapeIndex >= 0 && blendShapeIndex < skinnedMeshRenderer.sharedMesh.blendShapeCount;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            var renderer = vFXController.Renderers[_rendererIndex];

            if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                var blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
                _originalProperties[(Id, vFXController)] = 
                    skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
                skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, Mathf.Clamp(_weight, MIN_WEIGHT, MAX_WEIGHT));
            }

            base.Execute(vFXController);
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            Renderer renderer = vFXController.Renderers[_rendererIndex];

            if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                var blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
                var key = (Id, vFXController);
                if (_originalProperties.TryGetValue(key, out float originalWeight))
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, originalWeight);
                    _originalProperties.Remove(key);
                }
            }

            base.RevertVFX(vFXController);
        }
    }
}
