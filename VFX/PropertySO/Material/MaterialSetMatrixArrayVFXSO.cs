using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a matrix array property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetMatrixArray", menuName = "Sombra Studios/VFX/Material/Set Matrix Array")]
    public class MaterialSetMatrixArrayVFXSO : MaterialPropertyVFXSO<Matrix4x4[]>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasMatrix(id);
        }

        public override Matrix4x4[] GetProperty(int id, Material material)
        {
            return material.GetMatrixArray(id);
        }

        public override void SetProperty(int id, Matrix4x4[] value, Material material)
        {
            material.SetMatrixArray(id, value);
        }
    }
}
