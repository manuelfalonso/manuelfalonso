using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>  
    /// ScriptableObject for setting a texture property in a material.  
    /// </summary>  
    [CreateAssetMenu(fileName = "NewSetTexture", menuName = "Sombra Studios/VFX/Material/Set Texture")]
    public class MaterialSetTextureVFXSO : MaterialPropertyVFXSO<Texture>
    {
        public override bool HasProperty(int id, Material material)
        {
            return material.HasTexture(id);
        }

        public override Texture GetProperty(int id, Material material)
        {
            return material.GetTexture(id);
        }

        public override void SetProperty(int id, Texture value, Material material)
        {
            material.SetTexture(id, value);
        }
    }
}
