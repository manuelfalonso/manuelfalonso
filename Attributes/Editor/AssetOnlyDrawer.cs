#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Attributes.Editor
{
    /// <summary>
    /// Custom property drawer for the <see cref="AssetOnlyAttribute"/>.
    /// Ensures that the property can only reference assets and not scene objects.
    /// </summary>
    [CustomPropertyDrawer(typeof(AssetOnlyAttribute))]
    public class AssetOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Overrides the GUI rendering for the property.
        /// Ensures that only persistent (asset) objects can be assigned to the property.
        /// </summary>
        /// <param name="position">The rectangle on the screen to draw within.</param>
        /// <param name="property">The serialized property being drawn.</param>
        /// <param name="label">The label of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Disable scene object references
            Object assigned = EditorGUI.ObjectField(
                position,
                label,
                property.objectReferenceValue,
                fieldInfo.FieldType,
                false // disallowSceneObjects
            );

            // Assign the object only if it is persistent (an asset)
            if (assigned != null && EditorUtility.IsPersistent(assigned))
            {
                property.objectReferenceValue = assigned;
            }
            else if (assigned == null)
            {
                property.objectReferenceValue = null;
            }

            EditorGUI.EndProperty();
        }
    }
}
#endif
