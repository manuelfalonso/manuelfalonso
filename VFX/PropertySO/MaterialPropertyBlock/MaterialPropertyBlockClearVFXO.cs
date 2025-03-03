using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// ScriptableObject for clearing MaterialPropertyBlock values.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMPBClear", menuName = "Sombra Studios/VFX/Material Property Block/Clear")]
    public class MaterialPropertyBlockClearVFXO : MaterialPropertyBlockVFXSO<MaterialPropertyBlock>
    {
        protected override void StoreOriginalValue(MaterialPropertyBlock mpb)
        {
            var key = (_renderer, Id);
            if (!_originalProperties.ContainsKey(key))
            {
                if (_showLogs)
                {
                    Debug.Log($"Storing original value for key: {key}", this);
                }

                _originalProperties[key] = mpb;
            }
        }

        protected override void ApplyNewValue(MaterialPropertyBlock mpb)
        {
            if (_showLogs)
            {
                Debug.Log($"Applying new value for key: {(_renderer, Id)}", this);
            }

            mpb.Clear();
        }

        /// <summary>
        /// This method should not be called for this VFX property.
        /// </summary>
        /// <param name="mpb">The MaterialPropertyBlock to restore the original value to.</param>
        /// <param name="value">The original value to restore.</param>
        protected override void RestoreOriginalValue(MaterialPropertyBlock mpb, MaterialPropertyBlock value)
        {
            Debug.LogError("This method should not be called for this VFX property.", this);
        }
    }
}
