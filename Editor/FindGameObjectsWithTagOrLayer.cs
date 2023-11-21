using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Editor
{
    /// <summary>
    /// 1) Create an "Editor" folder in your project if you don't have one.
    /// 2) Place the script in the "Editor" folder.
    /// 3) In Unity, on the toolbar find the menu defined in MenuItem."
    /// 4) Set the target layer and click "Find".
    /// 5) The results will be displayed in the Unity console.
    /// </summary>
    // TODO: Consider runtime Tag or Layer asignations
    public class FindGameObjectsWithTagOrLayer : EditorWindow
    {
        /// <summary>
        /// The target for the search (Tag or Layer).
        /// </summary>
        private int _searchTarget;
        /// <summary>
        /// The selected search option (Project, Open Scene, Scenes in Build, All Scenes).
        /// </summary>
        private int _searchOption;
        
        /// <summary>
        /// The target LayerMask for the search.
        /// </summary>
        private LayerMask _targetLayer;
        /// <summary>
        /// The target Tag for the search.
        /// </summary>
        private string _targetTag;
        
        /// <summary>
        /// Indicates whether to include inactive GameObjects in the search.
        /// </summary>
        private bool _includeInactive = true;
        /// <summary>
        /// Flag indicating whether a search is currently in progress.
        /// </summary>
        private bool _isSearching = false;
        
        /// <summary>
        /// Dictionary to store GameObjects with Layer or Tag relationships.
        /// </summary>
        private Dictionary<GameObject, GameObject> _gameObjectsWithLayerOrTag = new Dictionary<GameObject, GameObject>();
        /// <summary>
        /// Error message to be displayed in the HelpBox.
        /// </summary>
        private string _helpBoxErrorMessage = string.Empty;
        
        /// <summary>
        /// Warning text to be displayed in the HelpBox.
        /// </summary>
        private const string WARNING_TEXT = "The search may take several minutes depending of the project size";
        /// <summary>
        /// Informational text to be displayed in the HelpBox.
        /// </summary>
        private const string INFO_TEXT = "Results will be shown on the console";
        
        // Error messages
        /// <summary>
        /// Text for an error message when trying to find an invalid scene.
        /// </summary>
        private const string FIND_OPEN_SCENE_TEXT = "Invalid scene";


        [MenuItem("Sombra Studios/Tools/Find GameObjects with Tag or Layer")]
        public static void ShowWindow() => GetWindow<FindGameObjectsWithTagOrLayer>("Tag/Layer Finder");


        private void OnGUI()
        {
            GUI.enabled = !_isSearching;

            _searchTarget = GUILayout.Toolbar(_searchTarget, new string[] { "Tag", "Layer" });
            _searchOption = GUILayout.Toolbar(
                _searchOption,
                new string[] { "Project", "Open Scene", "Scenes in Build", "All Scenes" });

            if (_searchTarget == 0)
            {
                _targetTag = EditorGUILayout.TagField("Target Tag", _targetTag);
            }
            else
            {
                _targetLayer = EditorGUILayout.LayerField("Target Layer", _targetLayer);
            }

            _includeInactive = EditorGUILayout.ToggleLeft("Include Inactive", _includeInactive);

            if (GUILayout.Button("Find")) { SearchOptionSwitch(); }

            EditorGUILayout.HelpBox(WARNING_TEXT, MessageType.Warning);
            EditorGUILayout.HelpBox(INFO_TEXT, MessageType.Info);
            if (string.IsNullOrEmpty(_helpBoxErrorMessage)) { return; }
            EditorGUILayout.HelpBox(_helpBoxErrorMessage, MessageType.Error);
        }

        /// <summary>
        /// Switches between different search options based on the selected toolbar option.
        /// </summary>
        private void SearchOptionSwitch()
        {
            _isSearching = true;

            switch (_searchOption)
            {
                case 0:
                    FindProject();
                    break;
                case 1:
                    FindOpenScene();
                    break;
                case 2:
                    FindScenesInBuild();
                    break;
                case 3:
                    FindAllScenes();
                    break;
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in the project.
        /// </summary>
        private void FindProject()
        {
            _gameObjectsWithLayerOrTag.Clear();
            _helpBoxErrorMessage = null;

            string[] prefabGuids = AssetDatabase.FindAssets("t:GameObject");
            foreach (string prefabGuid in prefabGuids)
            {
                string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                FindTagOrLayer(prefab);
            }

            ShowResults();
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in the currently open scene.
        /// </summary>
        private void FindOpenScene()
        {
            _gameObjectsWithLayerOrTag.Clear();
            _helpBoxErrorMessage = null;

            Scene scene = SceneManager.GetActiveScene();

            FindGameObjectsInScene(scene);

            ShowResults(scene.name);
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in all scenes included in the build settings.
        /// </summary>
        private void FindScenesInBuild()
        {
            _helpBoxErrorMessage = null;

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                _gameObjectsWithLayerOrTag.Clear();

                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                FindGameObjectsInScene(scene);

                ShowResults(scene.name);

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in all scenes of the project.
        /// </summary>
        private void FindAllScenes()
        {
            _helpBoxErrorMessage = null;

            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
            foreach (string sceneGuid in sceneGuids)
            {
                _gameObjectsWithLayerOrTag.Clear();

                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                FindGameObjectsInScene(scene);

                ShowResults(scene.name);

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in the given scene.
        /// </summary>
        /// <param name="scene">The scene to search for GameObjects.</param>
        private void FindGameObjectsInScene(Scene scene)
        {
            if (scene.IsValid())
            {
                GameObject[] sceneObjects = scene.GetRootGameObjects();

                foreach (GameObject obj in sceneObjects)
                {
                    FindTagOrLayer(obj);
                }
            }
            else
            {
                _helpBoxErrorMessage = FIND_OPEN_SCENE_TEXT;
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for tags or layers.</param>
        private void FindTagOrLayer(GameObject obj)
        {
            if (_searchTarget == 0)
            {
                FindTagInGameObject(obj);
            }
            else if (_searchTarget == 1)
            {
                FindLayerInGameObject(obj);
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for tags.</param>
        /// <returns>True if the tag is found, false otherwise.</returns>
        private bool FindTagInGameObject(GameObject obj)
        {
            if (obj != null)
            {
                Transform[] allTransforms = obj.GetComponentsInChildren<Transform>(_includeInactive);
                foreach (Transform t in allTransforms)
                {
                    if (t.gameObject.CompareTag(_targetTag))
                    {
                        return _gameObjectsWithLayerOrTag.TryAdd(obj, t.gameObject);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds GameObjects with the specified layer in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for layers.</param>
        /// <returns>True if the layer is found, false otherwise.</returns>
        private bool FindLayerInGameObject(GameObject obj)
        {
            if (obj != null)
            {
                Transform[] allTransforms = obj.GetComponentsInChildren<Transform>(_includeInactive);
                foreach (Transform t in allTransforms)
                {
                    if (t.gameObject.layer == _targetLayer)
                    {
                        return _gameObjectsWithLayerOrTag.TryAdd(obj, t.gameObject);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Displays the search results in the console, including scene information if provided.
        /// </summary>
        /// <param name="sceneName">The name of the scene (optional).</param>
        private void ShowResults(string sceneName = null)
        {
            if (_gameObjectsWithLayerOrTag.Count > 0)
            {
                Debug.Log($"Search result{(string.IsNullOrEmpty(sceneName) ? string.Empty : $" in {sceneName}")}");

                foreach (var obj in _gameObjectsWithLayerOrTag)
                {
                    string path = !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(obj.Key))
                        ? $"{AssetDatabase.GetAssetPath(obj.Key)}"
                        : $"{obj.Key}";

                    Debug.Log($"{path} - {obj.Value}");
                }
            }
            else
            {
                Debug.Log(string.IsNullOrEmpty(sceneName)
                    ? "No prefabs/gameobjects were found."
                    : $"No prefabs/gameobjects were found at {sceneName}.");
            }

            _isSearching = false;
        }
    }
}
