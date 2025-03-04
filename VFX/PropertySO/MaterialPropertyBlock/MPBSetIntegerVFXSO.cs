using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for setting a Integer property in a MaterialPropertyBlock.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBSetInteger", menuName = "Sombra Studios/VFX/Material Property Block/Set Integer")]
    public class MPBSetIntegerVFXSO : MaterialPropertyBlockPropertyVFXSO<int>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasInteger(_propertyID);
        }

        public override int GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetInteger(_propertyID)} for {_propertyID}", this);
            }

            // Get the current integer
            return mpb.GetInteger(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, int value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new integer
            mpb.SetInteger(_propertyID, value);
        }
    }
}
