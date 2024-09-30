using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityOnMouseMessagesHandler : MonoBehaviour
    {
        [Header("Input events")]
        [Tooltip("OnMouseDown is called when the user has pressed the mouse button while over the Collider.")]
        public UnityEvent OnMouseDownEvent = new ();
        [Tooltip("OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.")]
        public UnityEvent OnMouseDragEvent = new ();
        [Tooltip("Called when the mouse enters the Collider.")]
        public UnityEvent OnMouseEnterEvent = new ();
        [Tooltip("Called when the mouse is not any longer over the Collider.")]
        public UnityEvent OnMouseExitEvent = new ();
        [Tooltip("Called every frame while the mouse is over the Collider.")]
        public UnityEvent OnMouseOverEvent = new ();
        [Tooltip("OnMouseUp is called when the user has released the mouse button.")]
        public UnityEvent OnMouseUpEvent = new ();
        [Tooltip("OnMouseUpAsButton is only called when the mouse is released over the same Collider as " +
            "it was pressed.")]
        public UnityEvent OnMouseUpAsButtonEvent = new ();


        #region Unity Messages
        private void OnMouseDown()
        {
            OnMouseDownEvent.Invoke();
        }

        private void OnMouseDrag()
        {
            OnMouseDragEvent.Invoke();
        }

        private void OnMouseEnter()
        {
            OnMouseEnterEvent.Invoke();
        }

        private void OnMouseExit()
        {
            OnMouseExitEvent.Invoke();
        }

        private void OnMouseOver()
        {
            OnMouseOverEvent.Invoke();
        }

        private void OnMouseUp()
        {
            OnMouseUpEvent.Invoke();
        }

        private void OnMouseUpAsButton()
        {
            OnMouseUpAsButtonEvent.Invoke();
        }
        #endregion
    }
}
