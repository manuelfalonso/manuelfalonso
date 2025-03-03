using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    [CreateAssetMenu(fileName = "NewMPBSetColor", menuName = "Sombra Studios/VFX/Material Property Block/Set Color")]
    public class MPBSetColorVFXSO : MaterialPropertyBlockPropertyVFXSO<Color>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasColor(_propertyID);
        }

        public override Color GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetColor(_propertyID)} for {_propertyID}", this);
            }

            // Get the current color
            return mpb.GetColor(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Color value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new color
            mpb.SetColor(_propertyID, value);
        }
    }
}
