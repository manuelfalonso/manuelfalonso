#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SombraStudios.Shared.Scenes
{
    /// <summary>
    /// Represents a serializable reference to a scene. This allows for scene references in the editor that are safer than using strings.
    /// </summary>
    [System.Serializable]
    public class SceneObject
    {
        [SerializeField]
        private string _sceneName;

        /// <summary>
        /// Gets the name of the scene.
        /// </summary>
        public string Name => _sceneName;

        /// <summary>
        /// Allows a SceneObject to be implicitly converted to a string.
        /// </summary>
        /// <param name="sceneObject">The SceneObject to convert.</param>
        public static implicit operator string(SceneObject sceneObject)
        {
            return sceneObject._sceneName;
        }

        /// <summary>
        /// Allows a string to be implicitly converted to a SceneObject.
        /// </summary>
        /// <param name="sceneName">The scene name to convert.</param>
        public static implicit operator SceneObject(string sceneName)
        {
            return new SceneObject() { _sceneName = sceneName };
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Custom property drawer for SceneObject. Provides a user-friendly interface in the Unity Editor.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneObject))]
    public class SceneObjectEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneObj = GetSceneObject(property.FindPropertyRelative("_sceneName").stringValue);
            var newScene = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
            if (newScene == null)
            {
                var prop = property.FindPropertyRelative("_sceneName");
                prop.stringValue = "";
            }
            else
            {
                if (newScene.name != property.FindPropertyRelative("_sceneName").stringValue)
                {
                    var scnObj = GetSceneObject(newScene.name);
                    if (scnObj == null)
                    {
                        Debug.LogWarning("The scene " + newScene.name + " cannot be used. To use this scene add it to the build settings for the project.");
                    }
                    else
                    {
                        var prop = property.FindPropertyRelative("_sceneName");
                        prop.stringValue = newScene.name;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the SceneAsset object for a given scene name.
        /// </summary>
        /// <param name="sceneObjectName">The name of the scene to retrieve.</param>
        /// <returns>The SceneAsset object if found; otherwise, null.</returns>
        protected SceneAsset GetSceneObject(string sceneObjectName)
        {
            if (string.IsNullOrEmpty(sceneObjectName))
                return null;

            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
                if (scene.path.IndexOf(sceneObjectName) != -1)
                {
                    return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
                }
            }

            Debug.Log("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in the build settings.");
            return null;
        }
    }
#endif
}
