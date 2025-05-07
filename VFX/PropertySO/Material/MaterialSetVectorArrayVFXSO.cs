using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a vector array property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetVectorArray", menuName = "Sombra Studios/VFX/Material/Set Vector Array")]
    public class MaterialSetVectorArrayVFXSO : MaterialPropertyVFXSO<Vector4[]>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasVector(id);
        }

        public override Vector4[] GetProperty(int id, Material material)
        {
            return material.GetVectorArray(id);
        }

        public override void SetProperty(int id, Vector4[] value, Material material)
        {
            material.SetVectorArray(id, value);
        }
    }
}
