using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a float property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetFloat", menuName = "Sombra Studios/VFX/Material/Set Float")]
    public class MaterialSetFloatVFXSO : MaterialPropertyVFXSO<float>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasFloat(id);
        }

        public override float GetProperty(int id, Material material)
        {
            return material.GetFloat(id);
        }

        public override void SetProperty(int id, float value, Material material)
        {
            material.SetFloat(id, value);
        }
    }
}
