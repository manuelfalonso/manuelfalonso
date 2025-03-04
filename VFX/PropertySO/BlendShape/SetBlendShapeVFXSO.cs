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
        [Header(PROPERTIES_TITLE)]
        [SerializeField] private int _rendererIndex;
        [SerializeField] private string _blendShapeName;
        [SerializeField] private float _weight;

        public string BlendShapeName => _blendShapeName;
        public float Weight => _weight;

        private static readonly Dictionary<(SkinnedMeshRenderer, int), float> _originalWeights = new();
        private Renderer _renderer;

        public override bool CanExecute(BaseVFXController vFXController)
        {
            if (!base.CanExecute(vFXController)) return false;

            if (vFXController.Renderers.Count == 0) return false;

            if (_rendererIndex >= vFXController.Renderers.Count || vFXController.Renderers[_rendererIndex] == null)
                return false;

            if (vFXController.Renderers[_rendererIndex] is not SkinnedMeshRenderer skinnedMeshRenderer) return false;

            var index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
            return index >= 0 && index < skinnedMeshRenderer.sharedMesh.blendShapeCount;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            _renderer = vFXController.Renderers[_rendererIndex];

            if (_renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
                _originalWeights[(skinnedMeshRenderer, index)] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                skinnedMeshRenderer.SetBlendShapeWeight(index, Mathf.Clamp(_weight, 0f, 100f));
            }

            vFXController.AddToCurrentVFXProperties(this);
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            _renderer = vFXController.Renderers[_rendererIndex];

            if (_renderer is SkinnedMeshRenderer skinnedMeshRenderer)
            {
                var index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(_blendShapeName);
                var key = (skinnedMeshRenderer, index);
                if (_originalWeights.TryGetValue(key, out float originalWeight))
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(index, originalWeight);
                    _originalWeights.Remove(key);
                }
            }

            vFXController.RemoveFromCurrentVFXProperties(this);
        }
    }
}
