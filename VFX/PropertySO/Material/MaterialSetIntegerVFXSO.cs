using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a integer property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetInteger", menuName = "Sombra Studios/VFX/Material/Set Integer")]
    public class MaterialSetIntegerVFXSO : MaterialPropertyVFXSO<int>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasInteger(id);
        }

        public override int GetProperty(int id, Material material)
        {
            return material.GetInteger(id);
        }

        public override void SetProperty(int id, int value, Material material)
        {
            material.SetInteger(id, value);
        }
    }
}
