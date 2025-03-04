using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for setting a Vector Array property in a MaterialPropertyBlock.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBSetVectorArray", menuName = "Sombra Studios/VFX/Material Property Block/Set Vector Array")]
    public class MPBSetVectorArrayVFXSO : MaterialPropertyBlockPropertyVFXSO<Vector4[]>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasVector(_propertyID);
        }

        public override Vector4[] GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetVectorArray(_propertyID)} for {_propertyID}", this);
            }

            // Get the current vector array
            return mpb.GetVectorArray(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Vector4[] value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new vector array
            mpb.SetVectorArray(_propertyID, value);
        }
    }
}
