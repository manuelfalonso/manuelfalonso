using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.Values
{
    /// <summary>
    /// ScriptableObject that stores a string value.
    /// </summary>
    [CreateAssetMenu(fileName = "StringValue", menuName = "Sombra Studios/Values/String")]
    public class StringValueSO : ScriptableObject
    {
        /// <summary>
        /// The stored string value.
        /// </summary>
        public string Value;
    }
}
