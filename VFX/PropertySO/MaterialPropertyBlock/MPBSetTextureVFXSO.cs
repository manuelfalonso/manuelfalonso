using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    [CreateAssetMenu(fileName = "NewMPBSetTexture", menuName = "Sombra Studios/VFX/Material Property Block/Set Texture")]
    public class MPBSetTextureVFXSO : MaterialPropertyBlockPropertyVFXSO<Texture>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasTexture(_propertyID);
        }

        public override Texture GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetTexture(_propertyID)} for {_propertyID}", this);
            }

            // Get the current texture
            return mpb.GetTexture(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Texture value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new texture
            mpb.SetTexture(_propertyID, value);
        }
    }
}
