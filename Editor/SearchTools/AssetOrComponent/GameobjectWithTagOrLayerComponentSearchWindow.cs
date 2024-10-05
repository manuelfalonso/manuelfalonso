using SombraStudios.Shared.Editor.SearchTools.AssetOrComponent.Base;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.SearchTools.AssetOrComponent
{
    /// <summary>
    /// Custom Editor window to search for GameObjects with a specific tag or layer.
    /// </summary>
    public class GameobjectWithTagOrLayerComponentSearchWindow : ComponentSearchWindow<Transform>
    {
        private string _tagTarget;
        private LayerMask _layerTarget;
        private TargetType _targetType;

        /// <summary>
        /// Opens the Tag or Layer search window from the Unity Editor menu.
        /// </summary>
        [MenuItem("Sombra Studios/Tools/Search Tools/Tag or Layer")]
        public static void OpenWindow()
        {
            GetWindow<GameobjectWithTagOrLayerComponentSearchWindow>("Find Tag/Layer");
        }

        #region Override Methods
        /// <summary>
        /// Draws the GUI for selecting the type of search and the target tag or layer.
        /// </summary>
        protected override void OnGUI()
        {
            SelectTypeOfSearch();
            SelectTarget();
            base.OnGUI();
        }

        /// <summary>
        /// Searches within a specific GameObject for GameObjects with the specified tag or layer.
        /// </summary>
        /// <param name="go">The GameObject to search in.</param>
        protected override void Find(GameObject go)
        {
            FindTagOrLayer(go);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Selects the type of search (by tag or by layer).
        /// </summary>
        private void SelectTypeOfSearch()
        {
            _targetType = (TargetType)EditorGUILayout.EnumPopup("Search by", _targetType);
        }

        /// <summary>
        /// Selects the target tag or layer based on the selected type of search.
        /// </summary>
        private void SelectTarget()
        {
            if (_targetType is TargetType.Layer)
            {
                _layerTarget = EditorGUILayout.LayerField("Target Layer", _layerTarget);
            }
            else
            {
                _tagTarget = EditorGUILayout.TagField("Target Tag", _tagTarget);
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag or layer in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for tags or layers.</param>
        private void FindTagOrLayer(GameObject obj)
        {
            switch (_targetType)
            {
                default:
                case TargetType.Tag:
                    FindTagInGameObject(obj);
                    break;
                case TargetType.Layer:
                    FindLayerInGameObject(obj);
                    break;
            }
        }

        /// <summary>
        /// Finds GameObjects with the specified tag in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for tags.</param>
        /// <returns>True if the tag is found, false otherwise.</returns>
        private bool FindTagInGameObject(GameObject obj)
        {
            if (obj == null) { return false; }

            var allTransforms = obj.GetComponentsInChildren<Transform>(_searchInactiveObjects);

            return (from t in allTransforms
                    where t.gameObject.CompareTag(_tagTarget)
                    select TryAdd(t)).FirstOrDefault();
        }

        /// <summary>
        /// Finds GameObjects with the specified layer in the given GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to search for layers.</param>
        /// <returns>True if the layer is found, false otherwise.</returns>
        private bool FindLayerInGameObject(GameObject obj)
        {
            if (obj == null) { return false; }

            var allTransforms = obj.GetComponentsInChildren<Transform>(_searchInactiveObjects);

            return (from t in allTransforms
                    where t.gameObject.layer == _layerTarget
                    select TryAdd(t)).FirstOrDefault();
        }

        /// <summary>
        /// Tries to add the Transform to the search results if it doesn't already exist.
        /// </summary>
        /// <param name="transform">The Transform to add.</param>
        /// <returns>True if the Transform was added, false otherwise.</returns>
        private bool TryAdd(Transform transform)
        {
            if (transform == null) { return false; }

            bool alreadyExists = _searchResults.Exists(result => result.Result == transform);
            if (alreadyExists) { return false; }

            // Add additional information if necessary
            var assetInfo = AddAdditionalResultInfo(transform);

            // Add the search result to the list
            _searchResults.Add(assetInfo);

            return true;
        }
        #endregion
    }

    /// <summary>
    /// Enum to specify the type of search (by tag or by layer).
    /// </summary>
    public enum TargetType
    {
        Tag,
        Layer
    }
}
