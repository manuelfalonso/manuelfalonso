#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimatorParameterReference.Editor
{
    /// <summary>
    /// Custom editor for the AnimatorParameterReferenceSO ScriptableObject.
    /// Provides a user-friendly interface in the Unity Inspector to select Animator parameters.
    /// </summary>
    [CustomEditor(typeof(AnimatorParameterReferenceSO))]
    public class AnimatorParameterReferenceSOEditor : UnityEditor.Editor
    {
        private const string ANIMATOR_CONTROLLER_FIELD_LABEL = "Animator Controller";
        private const string PARAMETER_TYPE_FIELD_LABEL = "Parameter Type";
        private const string PARAMETER_NAME_FIELD_LABEL = "Parameter Name";

        public override void OnInspectorGUI()
        {
            var targetSO = (AnimatorParameterReferenceSO)target;

            EditorGUI.BeginChangeCheck();

            // AnimatorController field
            targetSO.AnimatorController = (UnityEditor.Animations.AnimatorController)EditorGUILayout.ObjectField(
                ANIMATOR_CONTROLLER_FIELD_LABEL,
                targetSO.AnimatorController,
                typeof(UnityEditor.Animations.AnimatorController),
                false);

            if (targetSO.AnimatorController != null)
            {
                DrawParameterTypeDropdown(targetSO);
                DrawParameterNameDropdown(targetSO);
            }
            else
            {
                EditorGUILayout.HelpBox("Assign an AnimatorController to view parameters.", MessageType.Info);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(targetSO);
            }
        }

        private void DrawParameterTypeDropdown(AnimatorParameterReferenceSO targetSO)
        {
            var parameterTypeValues = Enum.GetValues(typeof(AnimatorControllerParameterType))
                .Cast<AnimatorControllerParameterType>()
                .ToArray();
            int currentTypeIndex = Array.IndexOf(parameterTypeValues, targetSO.ParameterType);
            int selectedTypeIndex = EditorGUILayout.Popup(
                PARAMETER_TYPE_FIELD_LABEL,
                currentTypeIndex,
                parameterTypeValues.Select(t => t.ToString()).ToArray());

            if (selectedTypeIndex >= 0 && selectedTypeIndex < parameterTypeValues.Length)
            {
                targetSO.ParameterType = parameterTypeValues[selectedTypeIndex];
            }
        }

        private void DrawParameterNameDropdown(AnimatorParameterReferenceSO targetSO)
        {
            var validParams = targetSO.AnimatorController.parameters
                .Where(p => p.type == targetSO.ParameterType)
                .ToArray();

            if (validParams.Length > 0)
            {
                string[] options = validParams.Select(p => p.name).ToArray();
                int currentIndex = Array.FindIndex(validParams, p => p.name == targetSO.ParameterName);
                int selected = EditorGUILayout.Popup(PARAMETER_NAME_FIELD_LABEL, currentIndex, options);

                if (currentIndex == -1 && !string.IsNullOrEmpty(targetSO.ParameterName))
                {
                    EditorGUILayout.HelpBox(
                        $"Selected parameter \"{targetSO.ParameterName}\" not found in the Animator Controller or type doesn't match.",
                        MessageType.Warning
                    );
                }

                if (selected >= 0 && selected < options.Length)
                {
                    targetSO.ParameterName = options[selected];
                }
            }
            else
            {
                EditorGUILayout.HelpBox($"No parameters of type {targetSO.ParameterType} found.", MessageType.Warning);
            }
        }
    }
}
#endif
