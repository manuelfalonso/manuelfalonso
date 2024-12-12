using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Tools.Search.AssetOrComponent
{
    /// <summary>  
    /// Abstract class for creating a custom Editor window to search for assets of type T.  
    /// </summary>  
    /// <typeparam name="T">The type of asset to search for.</typeparam>  
    public abstract class AssetSearchWindow<T> : SearchWindow<T> where T : Object
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
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset == null) { continue; }

                // Add additional information if necessary  
                var assetInfo = AddAdditionalResultInfo(asset);

                // Add the search result to the list  
                _searchResults.Add(assetInfo);
            }
        }

        protected override void FindInScene(Scene scene)
        {
            if (scene.IsValid())
            {
                var rootGameObjects = scene.GetRootGameObjects();

                foreach (var go in rootGameObjects)
                {
                    Find(go);
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

            var assetGuids = AssetDatabase.FindAssets($"t:{nameof(T)}");

            foreach (var assetGuid in assetGuids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset == null) { continue; }

                // Add additional information if necessary  
                var assetInfo = AddAdditionalResultInfo(asset);

                // Add the search result to the list  
                _searchResults.Add(assetInfo);
            }
        }
        
        protected override void ShowResults()
        {
            if (_searchResults.Count <= 0)
            {
                _infoMessage = "No results found.";
                return;
            }
            else
            {
                _infoMessage = string.Empty;
            }

            GUILayout.Label("Search Results (" + _searchResults.Count + "):", EditorStyles.boldLabel);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            foreach (var asset in _searchResults)
            {
                if (asset == null) { continue; }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(asset.Result.name, asset.Result, typeof(T), true);
                foreach (var info in asset.AdditionalInfos)
                {
                    ShowAdditionalResultInfo(info);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
        #endregion

        #region Abstract Methods 
        protected abstract override void Find(GameObject go);
        #endregion
    }
}
