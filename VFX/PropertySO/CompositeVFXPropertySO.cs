using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    [CreateAssetMenu(menuName = "Sombra Studios/VFX/Composite")]
    public class CompositeVFXPropertySO : VFXPropertySO
    {
        [SerializeField] private List<VFXPropertySO> _vfxProperties;

        public override void Execute(BaseVFXController vFXController)
        {
            foreach (var vfx in _vfxProperties)
            {
                vfx.TryExecute(vFXController);
            }
        }

        public override void RevertVFX(BaseVFXController vFXController)
        {
            foreach (var vfx in _vfxProperties)
            {
                vfx.RevertVFX(vFXController);
            }
        }
    }
}
