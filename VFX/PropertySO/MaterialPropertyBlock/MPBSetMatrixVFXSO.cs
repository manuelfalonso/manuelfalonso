using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for setting a Matrix property in a MaterialPropertyBlock.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBSetMatrix", menuName = "Sombra Studios/VFX/Material Property Block/Set Matrix")]
    public class MPBSetMatrixVFXSO : MaterialPropertyBlockPropertyVFXSO<Matrix4x4>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasMatrix(_propertyID);
        }

        public override Matrix4x4 GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetMatrix(_propertyID)} for {_propertyID}", this);
            }

            // Get the current matrix
            return mpb.GetMatrix(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Matrix4x4 value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new matrix
            mpb.SetMatrix(_propertyID, value);
        }
    }
}
