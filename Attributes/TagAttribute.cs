using UnityEngine;

namespace SombraStudios.Shared.Attributes
{
    /// <summary>
    /// Displays a field with the list of Editor Tags
    /// Works only with string fields
    /// </summary>
    public class TagAttribute : PropertyAttribute
    {
        public bool UseDefaultTagFieldDrawer = false;
    }
}
