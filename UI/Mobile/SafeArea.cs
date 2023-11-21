using SombraStudios.Shared.Extensions;
using UnityEngine;

namespace SombraStudios.Shared.UI.Mobile
{
    /// <summary>
    /// Adjusts the RectTransform based on the device's safe area.
    /// </summary>
    public class SafeArea : MonoBehaviour
    {
        /// <summary>
        /// Reference to the RectTransform that will be adjusted based on the safe area.
        /// </summary>
        [Tooltip("Reference to the RectTransform that will be adjusted based on the safe area.")]
        [SerializeField] private RectTransform _rectTransform;


        /// <summary>
        /// Initializes the SafeArea and applies adjustments.
        /// </summary>
        private void Start()
        {
            // Ensure _rectTransform is initialized before applying the safe area
            this.SafeInit(ref _rectTransform);
            ApplySafeArea();
        }


        /// <summary>
        /// Applies the device's safe area to the RectTransform.
        /// </summary>
        private void ApplySafeArea()
        {
            // Get the device's safe area
            var safeArea = Screen.safeArea;

            // Convert safe area values to canvas space
            var minAnchor = safeArea.position;
            var maxAnchor = safeArea.position * safeArea.size;
            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;
            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;

            // Apply the safe area to the RectTransform
            _rectTransform.anchorMin = minAnchor;
            _rectTransform.anchorMax = maxAnchor;
        }
    }
}
