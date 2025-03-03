using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    [CreateAssetMenu(fileName = "NewMPBSetFloatArray", menuName = "Sombra Studios/VFX/Material Property Block/Set Float Array")]
    public class MPBSetFloatArrayVFXSO : MaterialPropertyBlockPropertyVFXSO<float[]>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasFloat(_propertyID);
        }

        public override float[] GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetFloatArray(_propertyID)} for {_propertyID}", this);
            }

            // Get the current float array
            return mpb.GetFloatArray(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, float[] value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new float array
            mpb.SetFloatArray(_propertyID, value);
        }
    }
}
