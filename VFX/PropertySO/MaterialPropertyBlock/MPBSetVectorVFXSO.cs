using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for setting a Vector property in a MaterialPropertyBlock.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBSetVector", menuName = "Sombra Studios/VFX/Material Property Block/Set Vector")]
    public class MPBSetVectorVFXSO : MaterialPropertyBlockPropertyVFXSO<Vector4>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasVector(_propertyID);
        }

        public override Vector4 GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetVector(_propertyID)} for {_propertyID}", this);
            }

            // Get the current vector
            return mpb.GetVector(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Vector4 value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new vector
            mpb.SetVector(_propertyID, value);
        }
    }
}
