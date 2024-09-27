using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Editor.SearchTools.Base
{
    /// <summary>  
    /// Abstract class for creating a custom Editor window to search for components of type T.  
    /// </summary>  
    /// <typeparam name="T">The type of component to search for.</typeparam>  
    public abstract class ComponentSearchWindow<T> : SearchWindow<T> where T : Component
    {
        #region Override Methods
        protected override void FindFolder()
        {
            if (_selectedFolder == null)
            {
                _errorMessage = "Please select a folder to search in.";
                return;
            }

            _searchResults.Clear();

            // Get the path of the selected folder
            var folderPath = AssetDatabase.GetAssetPath(_selectedFolder);

            // Get all asset paths within the selected folder
            var allAssetPaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            foreach (var assetPath in allAssetPaths)
            {
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (go == null) { continue; }
                Find(go);
            }
        }

        protected override void FindInScene(Scene scene)
        {
            if (scene.IsValid())
            {
                var sceneObjects = scene.GetRootGameObjects();

                foreach (var obj in sceneObjects)
                {
                    Find(obj);
                }
            }
            else
            {
                _errorMessage = "Scene is not valid.";
            }
        }

        protected override void FindProject()
        {
            _searchResults.Clear();

            var prefabGuids = AssetDatabase.FindAssets($"t:Prefab");

            foreach (var prefabGuid in prefabGuids)
            {
                var prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                Find(prefab);
            }
        }

        protected override void ShowResults()
        {
            if (_searchResults.Count <= 0)
            {
                _errorMessage = "No results found.";
                return;
            }

            GUILayout.Label("Search Results (" + _searchResults.Count + "):", EditorStyles.boldLabel);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            foreach (var go in _searchResults)
            {
                if (go == null) { continue; }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(go.Result.name, go.Result, typeof(GameObject), true);
                foreach (var info in go.AdditionalInfos)
                {
                    ShowAdditionalResultInfo(info);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
        #endregion


        #region Abstract Methods
        protected abstract override void Find(GameObject prefab);
        #endregion
    }
}
