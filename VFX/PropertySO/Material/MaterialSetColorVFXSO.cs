using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a color property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetColor", menuName = "Sombra Studios/VFX/Material/Set Color")]
    public class MaterialSetColorVFXSO : MaterialPropertyVFXSO<Color>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasColor(id);
        }

        public override Color GetProperty(int id, Material material)
        {
            return material.GetColor(id);
        }

        public override void SetProperty(int id, Color value, Material material)
        {
            material.SetColor(id, value);
        }
    }
}
