using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a matrix property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetMatrix", menuName = "Sombra Studios/VFX/Material/Set Matrix")]
    public class MaterialSetMatrixVFXSO : MaterialPropertyVFXSO<Matrix4x4>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasMatrix(id);
        }

        public override Matrix4x4 GetProperty(int id, Material material)
        {
            return material.GetMatrix(id);
        }

        public override void SetProperty(int id, Matrix4x4 value, Material material)
        {
            material.SetMatrix(id, value);
        }
    }
}
