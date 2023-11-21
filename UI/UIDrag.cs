using UnityEngine;
using UnityEngine.EventSystems;

namespace SombraStudios.Shared.UI
{
    /// <summary>
    /// Drag UI elements
    /// In the Event Trigger create Drag and Drop events
    /// Assign this scripts and their corresponding methods
    /// </summary>
    [RequireComponent(typeof(EventTrigger))]
    public class UIDrag : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        public void OnDrag()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            transform.position = canvas.transform.TransformPoint(position);
        }

        public void OnDrop()
        {
            Debug.Log("dropped");
        }
    }
}
