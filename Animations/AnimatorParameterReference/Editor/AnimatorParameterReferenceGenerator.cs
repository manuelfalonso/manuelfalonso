#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace SombraStudios.Shared.Animations.AnimatorParameterReference.Editor
{
    /// <summary>
    /// Utility class for generating parameter reference assets for an AnimatorController.
    /// </summary>
    public static class AnimatorParameterReferenceGenerator
    {
        private const string MENU_NAME = "Assets/Sombra Studios/Generate Animator Parameter References";
        private const int MENU_PRIORITY = 1000;

        /// <summary>
        /// Generates parameter reference assets for the selected AnimatorController.
        /// </summary>
        [MenuItem(MENU_NAME, false, MENU_PRIORITY)]
        private static void GenerateAnimatorParameterReferenceAssets()
        {
            if (Selection.activeObject is not UnityEditor.Animations.AnimatorController animatorController)
            {
                EditorUtility.DisplayDialog("Invalid Selection", "Please select an AnimatorController asset.", "OK");
                return;
            }

            string controllerPath = AssetDatabase.GetAssetPath(animatorController);
            string folderPath = Path.GetDirectoryName(controllerPath);
            string controllerName = Path.GetFileNameWithoutExtension(controllerPath);

            var parameters = animatorController.parameters;
            if (parameters.Length == 0)
            {
                Debug.LogWarning($"AnimatorController '{controllerName}' has no parameters to generate references for.");
                return;
            }

            foreach (var parameter in parameters)
            {
                CreateParameterReferenceAsset(animatorController, parameter, folderPath, controllerName);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Created parameter references for {animatorController.parameters.Length} parameters in '{controllerName}'.");
        }

        /// <summary>
        /// Creates a parameter reference asset for a specific parameter in the AnimatorController.
        /// </summary>
        /// <param name="animatorController">The AnimatorController containing the parameter.</param>
        /// <param name="parameter">The parameter to create a reference for.</param>
        /// <param name="folderPath">The folder path where the asset will be created.</param>
        /// <param name="controllerName">The name of the AnimatorController.</param>
        private static void CreateParameterReferenceAsset(
            UnityEditor.Animations.AnimatorController animatorController,
            AnimatorControllerParameter parameter,
            string folderPath,
            string controllerName)
        {
            var asset = ScriptableObject.CreateInstance<AnimatorParameterReferenceSO>();
#if UNITY_EDITOR
            asset.AnimatorController = animatorController;
#endif
            asset.ParameterType = parameter.type;
            asset.ParameterName = parameter.name;

            string safeName = $"{controllerName}_{parameter.type}_{parameter.name}";
            string assetPath = Path.Combine(folderPath, safeName + ".asset");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            AssetDatabase.CreateAsset(asset, assetPath);
        }

        /// <summary>
        /// Validates if the selected object is an AnimatorController.
        /// </summary>
        /// <returns>True if the selected object is an AnimatorController; otherwise, false.</returns>
        [MenuItem(MENU_NAME, true)]
        private static bool ValidateGenerateAnimatorParameterReferenceAssets()
        {
            return Selection.activeObject is UnityEditor.Animations.AnimatorController;
        }
    }
}
#endif
