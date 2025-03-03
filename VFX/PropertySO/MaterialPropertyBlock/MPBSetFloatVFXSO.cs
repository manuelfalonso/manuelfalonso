using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    [CreateAssetMenu(fileName = "NewMPBSetFloat", menuName = "Sombra Studios/VFX/Material Property Block/Set Float")]
    public class MPBSetFloatVFXSO : MaterialPropertyBlockPropertyVFXSO<float>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasFloat(_propertyID);
        }

        public override float GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetFloat(_propertyID)} for {_propertyID}", this);
            }

            // Get the current float
            return mpb.GetFloat(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, float value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new float
            mpb.SetFloat(_propertyID, value);
        }
    }
}
