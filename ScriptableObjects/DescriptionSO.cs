using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects
{
    /// <summary>
    /// This is a base ScriptableObject that adds a description field.
    /// </summary>
    public class DescriptionSO : ScriptableObject
    {
        [TextArea(5, 20)]
        [SerializeField] protected string m_Description;
    }
}
