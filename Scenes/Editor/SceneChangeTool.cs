#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Scenes.Editor
{
    /// <summary>
    /// Adds a dropdown to the Editor's main toolbar that switches between the scenes registered in
    /// Build Settings, without having to locate them in the Project window.
    /// </summary>
    public static class SceneChangeTool
    {
        private const string ElementPath = "Editor Utility/Scene Switcher";
        private const string EmptyLabel = "No Scene";
        private const string Tooltip = "Switch to a scene registered in Build Settings";
        private const string NoScenesLabel = "No scenes in Build Settings";

        private static MainToolbarDropdown _dropdown;

        /// <summary>
        /// Builds the toolbar element. Invoked by the Editor while the main toolbar is created.
        /// </summary>
        /// <returns>The scene switcher dropdown.</returns>
        [MainToolbarElement(ElementPath,
            defaultDockPosition = MainToolbarDockPosition.Middle,
            defaultDockIndex = 10)]
        private static MainToolbarElement CreateSceneDropdown()
        {
            _dropdown = new MainToolbarDropdown(GetContent(), ShowSceneMenu)
            {
                enabled = !Application.isPlaying
            };

            // The factory runs again after every domain reload, so drop the previous subscription first.
            EditorSceneManager.activeSceneChangedInEditMode -= OnActiveSceneChanged;
            EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChanged;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            return _dropdown;
        }

        /// <summary>
        /// Populates and shows the scene list as a dropdown menu.
        /// </summary>
        /// <param name="anchor">Screen rect of the dropdown button.</param>
        private static void ShowSceneMenu(Rect anchor)
        {
            var menu = new GenericMenu();
            var activePath = SceneManager.GetActiveScene().path;

            foreach (var buildScene in EditorBuildSettings.scenes)
            {
                if (string.IsNullOrEmpty(buildScene.path))
                {
                    continue;
                }

                var scenePath = buildScene.path;
                var label = new GUIContent(Path.GetFileNameWithoutExtension(scenePath));

                menu.AddItem(label, scenePath == activePath, () => OpenScene(scenePath));
            }

            if (menu.GetItemCount() == 0)
            {
                menu.AddDisabledItem(new GUIContent(NoScenesLabel));
            }

            menu.DropDown(anchor);
        }

        /// <summary>
        /// Opens a scene, prompting to save any modified scenes first.
        /// </summary>
        /// <param name="scenePath">Project relative path of the scene to open.</param>
        private static void OpenScene(string scenePath)
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }

        /// <summary>
        /// Refreshes the dropdown label when the active scene changes in edit mode.
        /// </summary>
        /// <param name="previous">Scene that was active before the change.</param>
        /// <param name="next">Scene that became active.</param>
        private static void OnActiveSceneChanged(Scene previous, Scene next)
        {
            if (_dropdown == null)
            {
                return;
            }

            _dropdown.content = GetContent();
        }

        /// <summary>
        /// Disables the dropdown while the Editor is in play mode.
        /// </summary>
        /// <param name="state">The play mode state that was entered.</param>
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (_dropdown == null)
            {
                return;
            }

            _dropdown.enabled = !EditorApplication.isPlayingOrWillChangePlaymode;
        }

        /// <summary>
        /// Builds the dropdown content from the currently active scene.
        /// </summary>
        /// <returns>Content showing the active scene name.</returns>
        private static MainToolbarContent GetContent()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var label = string.IsNullOrEmpty(sceneName) ? EmptyLabel : sceneName;

            return new MainToolbarContent(label, Tooltip);
        }
    }
}
#endif
