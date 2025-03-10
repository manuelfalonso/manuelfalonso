﻿using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for setting a Matrix Array property in a MaterialPropertyBlock.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBSetMatrixArray", menuName = "Sombra Studios/VFX/Material Property Block/Set Matrix Array")]
    public class MPBSetMatrixArrayVFXSO : MaterialPropertyBlockPropertyVFXSO<Matrix4x4[]>
    {
        public override bool HasProperty(MaterialPropertyBlock mpb)
        {
            return mpb.HasMatrix(_propertyID);
        }

        public override Matrix4x4[] GetProperty(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Getting property {mpb.GetMatrixArray(_propertyID)} for {_propertyID}", this);
            }

            // Get the current matrix array
            return mpb.GetMatrixArray(_propertyID);
        }

        public override void SetProperty(MaterialPropertyBlock mpb, Matrix4x4[] value)
        {
            if (_showLogs)
            {
                Debug.Log($"Setting property to {value} for {_propertyID}", this);
            }

            // Apply the new matrix array
            mpb.SetMatrixArray(_propertyID, value);
        }
    }
}
