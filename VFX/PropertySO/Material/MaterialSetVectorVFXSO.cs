using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a vector property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetVector", menuName = "Sombra Studios/VFX/Material/Set Vector")]
    public class MaterialSetVectorVFXSO : MaterialPropertyVFXSO<Vector4>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasVector(id);
        }

        public override Vector4 GetProperty(int id, Material material)
        {
            return material.GetVector(id);
        }

        public override void SetProperty(int id, Vector4 value, Material material)
        {
            material.SetVector(id, value);
        }
    }
}
