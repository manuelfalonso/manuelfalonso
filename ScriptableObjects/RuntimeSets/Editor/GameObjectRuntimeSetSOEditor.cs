using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets.Editor
{
    /// <summary>
	/// This class overrides the default Editor behavior to show the elements of the runtime set.
    /// This fixes the 'Type Mismatch' that appears in the Inspector when an Asset tries to show a 
    /// GameObject from the Hierarchy.
	/// </summary>
    [CustomEditor(typeof(GameObjectRuntimeSetSO))]
    public class GameObjectRuntimeSetSOEditor : UnityEditor.Editor
    {
        // Reference to the target, cast as a RuntimeSet
        private GameObjectRuntimeSetSO _runtimeSet;

        // UI to show the contents of a list
        private ListView _itemsListView;

        // Label and counter for items in the list
        private Label _listLabel;

        private void OnEnable()
        {
            // Reference to the runtime set we are inspecting
            if (_runtimeSet == null)
                _runtimeSet = target as GameObjectRuntimeSetSO;

            // Subscribe to the ItemsChanged event.
            _runtimeSet.ItemsChanged += OnItemsChanged;
        }

        private void OnDisable()
        {
            // Unsubscribe from the ItemsChanged event to prevent errors.
            _runtimeSet.ItemsChanged -= OnItemsChanged;
        }

        // Draw the custom Inspector
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Draw default elements in the inspector
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            // Add a spacer
            var spaceElement = new VisualElement();
            spaceElement.style.marginBottom = 10;
            root.Add(spaceElement);

            // Add a title label
            _listLabel = new Label();
            _listLabel.text = "Runtime Set Items:";
            root.Add(_listLabel);

            // Add a horizontal line / spacer
            var separator = new VisualElement();
            separator.style.borderBottomWidth = 1;
            separator.style.borderBottomColor = Color.grey;
            separator.style.marginBottom = 10;
            root.Add(separator);

            _itemsListView = new ListView(_runtimeSet.Items, 20, MakeItem, BindItem);

            // Add the ListView to the root element
            root.Add(_itemsListView);
            return root;
        }

        /// <summary>
        /// Generates a simple text VisualElement for each element in the ListView
        /// </summary>
        /// <returns>The empty visual element created</returns>
        private VisualElement MakeItem()
        {
            var element = new VisualElement();
            var label = new Label();
            element.Add(label);
            return element;
        }

        /// <summary>
        /// Logic to sync the ListView label text to the RuntimeSet.Items list 
        /// </summary>
        /// <param name="element">The element to sync its label.</param>
        /// <param name="index">The integer index in the RuntimeSet Items list.</param>
        private void BindItem(VisualElement element, int index)
        {
            if (_runtimeSet.Items.Count == 0)
                return;

            var item = _runtimeSet.Items[index];

            Label label = (Label)element.ElementAt(0);
            label.text = item ? item.name : "<null>";

            // Attach a ClickEvent to the label
            label.RegisterCallback<MouseDownEvent>(evt =>
            {
                // Ping the item in the Hierarchy
                EditorGUIUtility.PingObject(item);

                // Alternatively, change this so the Selection.activeObject = item;
            });
        }

        /// <summary>
        /// Invokes every time the runtime set list changes 
        /// </summary>
        private void OnItemsChanged()
        {
            // Format the label with an item counter
            if (_listLabel != null)
            {
                string message = "Runtime Set Items";
                message = (_runtimeSet == null) ? message + ":" : message + " (Count " + _runtimeSet.Items.Count + "):";
                _listLabel.text = message;
            }

            // Force refresh of the Inspector when the Items list changes
            if (_itemsListView != null)
                _itemsListView.Rebuild();
        }
    }
}
