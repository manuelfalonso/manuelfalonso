using System;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);

            string valueString;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    valueString = property.intValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    valueString = property.floatValue.ToString("0.0000");
                    break;
                case SerializedPropertyType.String:
                    valueString = property.stringValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    valueString = property.boolValue.ToString();
                    break;
                case SerializedPropertyType.Enum:
                    valueString = property.enumNames[property.enumValueIndex];
                    break;
                case SerializedPropertyType.ObjectReference:
                    try { valueString = property.objectReferenceValue.ToString(); }
                    catch (NullReferenceException) { valueString = "None (Game Object)"; }
                    break;
                default:
                    valueString = "(Not supported)";
                    break;
            }

            EditorGUI.LabelField(position, label.text, valueString);
        }
    }
}
