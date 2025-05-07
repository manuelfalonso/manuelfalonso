using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a float array property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetFloatArray", menuName = "Sombra Studios/VFX/Material/Set Float Array")]
    public class MaterialSetFloatArrayVFXSO : MaterialPropertyVFXSO<float[]>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasFloat(id);
        }

        public override float[] GetProperty(int id, Material material)
        {
            return material.GetFloatArray(id);
        }

        public override void SetProperty(int id, float[] value, Material material)
        {
            material.SetFloatArray(id, value);
        }
    }
}
