using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Tools.Search.AssetOrComponent
{
    /// <summary>
    /// Abstract base class for creating a custom Editor window to find various assets or objects.
    /// </summary>
    public abstract class SearchWindow<T> : EditorWindow
    {
        /// <summary>
        /// Class to hold additional information for search results.
        /// </summary>
        protected class ResultsAdditionalInfo { }

        /// <summary>
        /// Class to hold search results.
        /// </summary>
        protected class SearchResults
        {
            /// <summary>
            /// The search result.
            /// </summary>
            public T Result;

            /// <summary>
            /// List of additional information related to the search result.
            /// </summary>
            public List<ResultsAdditionalInfo> AdditionalInfos = new();
        }

        /// <summary>
        /// List of search results.
        /// </summary>
        protected readonly List<SearchResults> _searchResults = new();

        /// <summary>
        /// The search option selected by the user.
        /// </summary>
        protected SearchOptions _searchOption;

        /// <summary>
        /// Whether to search inactive objects.
        /// </summary>
        protected bool _searchInactiveObjects = true;

        /// <summary>
        /// The folder selected by the user for searching.
        /// </summary>
        protected DefaultAsset _selectedFolder;

        /// <summary>
        /// Indicates if a search is currently in progress.
        /// </summary>
        protected bool _isSearching;

        /// <summary>
        /// Information message to display.
        /// </summary>
        protected string _infoMessage = string.Empty;

        /// <summary>
        /// Warning message to display.
        /// </summary>
        protected string _warningMessage = string.Empty;

        /// <summary>
        /// Error message to display if any issues occur during the search.
        /// </summary>
        protected string _errorMessage = string.Empty;

        /// <summary>
        /// Scroll position for the results display.
        /// </summary>
        protected Vector2 _scrollPosition;

        #region Unity Messages
        /// <summary>
        /// Unity callback for drawing the GUI.
        /// </summary>
        protected virtual void OnGUI()
        {
            SelectSearchOptions();
            SearchInactiveObjects();
            SelectFolder();
            FindButton();

            ShowResults();

            InfoMessage();
            WarningMessage();
            ErrorMessage();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Displays the Find button and initiates the search when clicked.
        /// </summary>
        protected void FindButton()
        {
            if (GUILayout.Button("Find"))
            {
                if (_isSearching) { return; }

                _isSearching = true;

                _errorMessage = string.Empty;

                switch (_searchOption)
                {
                    default:
                    case SearchOptions.Folder:
                        FindFolder();
                        break;
                    case SearchOptions.Project:
                        FindProject();
                        break;
                    case SearchOptions.OpenScene:
                        FindOpenScene();
                        break;
                    case SearchOptions.ScenesInBuild:
                        FindScenesInBuild();
                        break;
                    case SearchOptions.AllScenes:
                        FindAllScenes();
                        break;
                }

                _isSearching = false;
            }
        }

        /// <summary>
        /// Displays the search options toolbar.
        /// </summary>
        protected void SelectSearchOptions()
        {
            var options = Enum.GetNames(typeof(SearchOptions));
            _searchOption = (SearchOptions)GUILayout.Toolbar((int)_searchOption, options);
        }

        /// <summary>
        /// Displays a toggle for searching inactive objects.
        /// </summary>
        protected void SearchInactiveObjects()
        {
            _searchInactiveObjects = EditorGUILayout.Toggle("Search Inactive Objects", _searchInactiveObjects);
        }

        /// <summary>
        /// Displays a field for selecting a folder if the search option is set to Folder.
        /// </summary>
        protected void SelectFolder()
        {
            if (_searchOption != SearchOptions.Folder) { return; }

            _selectedFolder =
                EditorGUILayout.ObjectField("Search Folder", _selectedFolder, typeof(DefaultAsset), false) as
                    DefaultAsset;
        }

        /// <summary>
        /// Displays an information message if there is any.
        /// </summary>
        protected void InfoMessage()
        {
            EditorGUILayout.BeginVertical();

            if (!string.IsNullOrEmpty(_infoMessage))
            {
                EditorGUILayout.HelpBox(_infoMessage, MessageType.Info);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Displays a warning message if there is any.
        /// </summary>
        protected void WarningMessage()
        {
            EditorGUILayout.BeginVertical();

            if (!string.IsNullOrEmpty(_warningMessage))
            {
                EditorGUILayout.HelpBox(_warningMessage, MessageType.Warning);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Displays an error message if there is any.
        /// </summary>
        protected void ErrorMessage()
        {
            EditorGUILayout.BeginVertical();

            if (!string.IsNullOrEmpty(_errorMessage))
            {
                EditorGUILayout.HelpBox(_errorMessage, MessageType.Error);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Searches the currently open scene.
        /// </summary>
        protected void FindOpenScene()
        {
            _searchResults.Clear();

            var scene = SceneManager.GetActiveScene();

            FindInScene(scene);
        }

        /// <summary>
        /// Searches all scenes included in the build settings.
        /// </summary>
        protected void FindScenesInBuild()
        {
            _searchResults.Clear();

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                FindInScene(scene);

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>
        /// Searches all scenes in the project.
        /// </summary>
        protected void FindAllScenes()
        {
            _searchResults.Clear();

            var sceneGuids = AssetDatabase.FindAssets("t:Scene");
            foreach (var sceneGuid in sceneGuids)
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                FindInScene(scene);

                EditorSceneManager.CloseScene(scene, true);
            }
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Adds additional information to the search results.
        /// </summary>
        /// <param name="asset">The asset to add additional information for.</param>
        /// <returns>The search result with additional information.</returns>
        protected virtual SearchResults AddAdditionalResultInfo(T asset)
        {
            // Override this method to add additional information to the search results

            return new SearchResults()
            {
                Result = asset,
                AdditionalInfos = new List<ResultsAdditionalInfo>()
            };
        }

        /// <summary>
        /// Displays additional information in the search results.
        /// </summary>
        /// <param name="assetInfo">The additional information to display.</param>
        protected virtual void ShowAdditionalResultInfo(ResultsAdditionalInfo assetInfo)
        {
            // Override this method to display additional information in the search results
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Searches within the selected folder.
        /// </summary>
        protected abstract void FindFolder();

        /// <summary>
        /// Searches the entire project.
        /// </summary>
        protected abstract void FindProject();

        /// <summary>
        /// Displays the search results.
        /// </summary>
        protected abstract void ShowResults();

        /// <summary>
        /// Searches within a specific scene.
        /// </summary>
        /// <param name="scene">The scene to search in.</param>
        protected abstract void FindInScene(Scene scene);

        /// <summary>
        /// Searches within a specific GameObject.
        /// </summary>
        /// <param name="go">The GameObject to search in.</param>
        protected abstract void Find(GameObject go);
        #endregion
    }

    /// <summary>
    /// Enumeration of the different search options available.
    /// </summary>
    public enum SearchOptions
    {
        Folder,
        Project,
        OpenScene,
        ScenesInBuild,
        AllScenes,
    }
}
