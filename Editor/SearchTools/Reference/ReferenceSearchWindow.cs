using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Editor.SearchTools.Reference
{
    /// <summary>
    /// A window for finding references to a specific component or gameObject in the current scene or prefab.
    /// </summary>
    public class ReferenceSearchWindow : EditorWindow
    {
        /// <summary>
        /// The target component to search for references.
        /// </summary>
        private Component _targetComponent;

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
        /// Scroll position for the results list.
        /// </summary>
        private Vector2 _scrollPos;

        /// <summary>
        /// List of components that reference the target component.
        /// </summary>
        private readonly List<Component> _references = new();

        /// <summary>
        /// Opens the Reference Finder window.
        /// </summary>
        [MenuItem("Tools/Reference Finder")]
        public static void OpenWindow()
        {
            GetWindow<ReferenceSearchWindow>("Reference Finder");
        }

        /// <summary>
        /// Draws the GUI for the window.
        /// </summary>
        private void OnGUI()
        {
            SelectTarget();
            FindButton();
            ClearResultsButton();
            ShowResults();
            InfoMessage();
            WarningMessage();
            ErrorMessage();
        }

        /// <summary>
        /// Displays the target field.
        /// </summary>
        private void SelectTarget()
        {
            // Select the component to search for references
            _targetComponent = (Component)EditorGUILayout.ObjectField("Target", _targetComponent, typeof(Component), true);
        }

        /// <summary>
        /// Displays the Find button and initiates the search when clicked.
        /// </summary>
        private void FindButton()
        {
            if (GUILayout.Button("Find References"))
            {
                if (_targetComponent != null)
                {
                    _warningMessage = string.Empty;
                    FindReferences(_targetComponent);
                }
                else
                {
                    _warningMessage = "Please select a target component.";
                }
            }
        }

        /// <summary>
        /// Displays the Clear Results button and clears the results when clicked.
        /// </summary>
        private void ClearResultsButton()
        {
            if (GUILayout.Button("Clear Results"))
            {
                if (_references.Count > 0)
                {
                    _references.Clear();
                    _warningMessage = string.Empty;
                }
                else
                {
                    _warningMessage = "No Results to clear";
                }
            }
        }

        /// <summary>
        /// Displays the search results.
        /// </summary>
        protected void ShowResults()
        {
            if (_references.Count <= 0)
            {
                _errorMessage = "No results found.";
                return;
            }

            _errorMessage = string.Empty;

            GUILayout.Label($"Search Results ({_references.Count}):", EditorStyles.boldLabel);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            // Display each reference found as an ObjectField
            foreach (var reference in _references)
            {
                EditorGUILayout.ObjectField("Reference", reference, typeof(GameObject), true);
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Displays an information message if there is any.
        /// </summary>
        protected void InfoMessage()
        {
            if (!string.IsNullOrEmpty(_infoMessage))
            {
                EditorGUILayout.HelpBox(_infoMessage, MessageType.Info);
            }
        }

        /// <summary>
        /// Displays a warning message if there is any.
        /// </summary>
        protected void WarningMessage()
        {
            if (!string.IsNullOrEmpty(_warningMessage))
            {
                EditorGUILayout.HelpBox(_warningMessage, MessageType.Warning);
            }
        }

        /// <summary>
        /// Displays an error message if there is any.
        /// </summary>
        protected void ErrorMessage()
        {
            if (!string.IsNullOrEmpty(_errorMessage))
            {
                EditorGUILayout.HelpBox(_errorMessage, MessageType.Error);
            }
        }

        /// <summary>
        /// Finds references to the specified component.
        /// </summary>
        /// <param name="component">The component to find references to.</param>
        private void FindReferences(Component component)
        {
            _references.Clear();

            // Check if we're in prefab mode
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if (prefabStage != null)
            {
                FindInPrefabMode(prefabStage);
            }
            else
            {
                FindInSceneMode();
            }
        }

        /// <summary>
        /// Finds references in prefab mode.
        /// </summary>
        /// <param name="prefabStage">The current prefab stage.</param>
        private void FindInPrefabMode(PrefabStage prefabStage)
        {
            // We're in prefab mode, search all components within the prefab instance root
            Component[] allComponents = prefabStage.prefabContentsRoot.GetComponentsInChildren<Component>(true);

            // Search for specific component reference
            foreach (Component c in allComponents)
            {
                SerializedObject serializedObject = new(c);
                SerializedProperty property = serializedObject.GetIterator();

                while (property.NextVisible(true))
                {
                    if (property.propertyType == SerializedPropertyType.ObjectReference &&
                        (property.objectReferenceValue == _targetComponent ||
                         property.objectReferenceValue == _targetComponent.gameObject))
                    {
                        _references.Add(c);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Finds references in scene mode.
        /// </summary>
        private void FindInSceneMode()
        {
            // We're in scene mode, search in all scene objects
            var scene = SceneManager.GetActiveScene();
            var allObjects = scene.GetRootGameObjects();

            foreach (GameObject obj in allObjects)
            {
                Component[] allComponents = obj.GetComponentsInChildren<Component>(true);

                // Search for specific component reference
                foreach (Component c in allComponents)
                {
                    SerializedObject serializedObject = new(c);
                    SerializedProperty property = serializedObject.GetIterator();

                    while (property.NextVisible(true))
                    {
                        if (property.propertyType == SerializedPropertyType.ObjectReference &&
                            (property.objectReferenceValue == _targetComponent ||
                             property.objectReferenceValue == _targetComponent.gameObject))
                        {
                            _references.Add(c);
                            break;
                        }
                    }
                }
            }
        }
    }
}
