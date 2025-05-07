using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a color array property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetColorArray", menuName = "Sombra Studios/VFX/Material/Set Color Array")]
    public class MaterialSetColorArrayVFXSO : MaterialPropertyVFXSO<Color[]>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasColor(id);
        }

        public override Color[] GetProperty(int id, Material material)
        {
            return material.GetColorArray(id);
        }

        public override void SetProperty(int id, Color[] value, Material material)
        {
            material.SetColorArray(id, value);
        }
    }
}
