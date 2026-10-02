using UnityEngine;

namespace SombraStudios.Shared.UI.SafeArea
{
    /// <summary>
    /// Fits a uGUI RectTransform inside the device safe area (notch, punch-hole camera, home indicator)
    /// by driving its anchors relative to its parent.
    /// </summary>
    /// <remarks>
    /// Put it on a child of a Screen Space canvas, usually stretched to fill it, and parent the content that must
    /// stay visible and touchable under it. Backgrounds meant to bleed to the screen edges stay outside.
    /// Applied in Play Mode only, so scenes and prefabs are never dirtied; preview it with the Device Simulator.
    /// The original anchors and offsets are restored when the component is disabled.
    /// </remarks>
    [AddComponentMenu("Sombra Studios/UI/Safe Area (uGUI)")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaUGUI : MonoBehaviour
    {
        [Tooltip("Edges that are pushed inside the safe area.")]
        [SerializeField] private SafeAreaEdges _edges = SafeAreaEdges.All;

        [Tooltip("Use the larger of the left and right insets on both sides, so content stays centred in landscape.")]
        [SerializeField] private bool _isHorizontallySymmetric;

        private readonly Vector3[] _parentCorners = new Vector3[4];

        private RectTransform _rectTransform;
        private RectTransform _parent;
        private Canvas _rootCanvas;

        private Vector2 _initialAnchorMin;
        private Vector2 _initialAnchorMax;
        private Vector2 _initialOffsetMin;
        private Vector2 _initialOffsetMax;

        private Rect _lastSafeArea;
        private Rect _lastContainer;
        private bool _isDirty = true;

        /// <summary>
        /// Edges that are pushed inside the safe area.
        /// </summary>
        public SafeAreaEdges Edges
        {
            get => _edges;
            set
            {
                _edges = value;
                ForceRefresh();
            }
        }

        /// <summary>
        /// Use the larger of the left and right insets on both sides.
        /// </summary>
        public bool IsHorizontallySymmetric
        {
            get => _isHorizontallySymmetric;
            set
            {
                _isHorizontallySymmetric = value;
                ForceRefresh();
            }
        }

        /// <summary>
        /// The insets applied on the last refresh, in screen pixels.
        /// </summary>
        public SafeAreaInsets CurrentInsets { get; private set; }

        /// <summary>
        /// Recalculates and applies the safe area immediately, even if nothing seems to have changed.
        /// </summary>
        public void ForceRefresh()
        {
            _isDirty = true;
            Refresh();
        }

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        private void OnEnable()
        {
            _initialAnchorMin = _rectTransform.anchorMin;
            _initialAnchorMax = _rectTransform.anchorMax;
            _initialOffsetMin = _rectTransform.offsetMin;
            _initialOffsetMax = _rectTransform.offsetMax;

            CacheHierarchy();
            ForceRefresh();
        }

        private void OnDisable()
        {
            _rectTransform.anchorMin = _initialAnchorMin;
            _rectTransform.anchorMax = _initialAnchorMax;
            _rectTransform.offsetMin = _initialOffsetMin;
            _rectTransform.offsetMax = _initialOffsetMax;
            CurrentInsets = SafeAreaInsets.Zero;
        }

        private void Update()
        {
            // There is no safe area changed event: rotation, resolution, split screen and the Device Simulator
            // all have to be detected by polling. The comparison is cheap enough to run every frame.
            Refresh();
        }

        private void OnRectTransformDimensionsChange()
        {
            // The canvas resizes after Update, so a rotation is only fully resolved once our rect follows it.
            if (isActiveAndEnabled && _rectTransform != null)
            {
                Refresh();
            }
        }

        private void OnTransformParentChanged()
        {
            if (isActiveAndEnabled)
            {
                CacheHierarchy();
                ForceRefresh();
            }
        }

        private void OnCanvasHierarchyChanged()
        {
            if (isActiveAndEnabled)
            {
                CacheHierarchy();
                ForceRefresh();
            }
        }

        private void OnValidate()
        {
            // Touching the RectTransform here sends OnRectTransformDimensionsChange, which Unity forbids during
            // OnValidate. Defer to the next Update instead.
            _isDirty = true;
        }

        private void CacheHierarchy()
        {
            _parent = transform.parent as RectTransform;
            Canvas canvas = GetComponentInParent<Canvas>(true);
            _rootCanvas = canvas != null ? canvas.rootCanvas : null;

            if (_parent == null || _rootCanvas == null)
            {
                Debug.LogWarning($"[{nameof(SafeAreaUGUI)}] '{name}' must be a child of a RectTransform " +
                    "inside a Canvas.", this);
            }
            else if (_rootCanvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.LogWarning($"[{nameof(SafeAreaUGUI)}] '{name}' is on a World Space canvas, where the " +
                    "screen safe area does not apply.", this);
            }
        }

        private void Refresh()
        {
            if (!TryGetParentScreenRect(out Rect container))
            {
                return;
            }

            Rect safeArea = Screen.safeArea;
            if (!_isDirty && safeArea == _lastSafeArea && container == _lastContainer)
            {
                return;
            }

            // State is stored before touching the anchors, which re-enters through OnRectTransformDimensionsChange.
            _lastSafeArea = safeArea;
            _lastContainer = container;
            _isDirty = false;

            CurrentInsets = SafeAreaCalculator.CalculateInsets(safeArea, container, _edges, _isHorizontallySymmetric);
            SafeAreaCalculator.CalculateAnchors(CurrentInsets, container.size,
                out Vector2 anchorMin, out Vector2 anchorMax);

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }

        private bool TryGetParentScreenRect(out Rect screenRect)
        {
            screenRect = default;
            if (_parent == null || _rootCanvas == null || _rootCanvas.renderMode == RenderMode.WorldSpace)
            {
                return false;
            }

            // Overlay canvases live in screen pixels already; a null camera makes the conversion a pass-through.
            Camera camera = _rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _rootCanvas.worldCamera;

            _parent.GetWorldCorners(_parentCorners);
            Vector2 min = RectTransformUtility.WorldToScreenPoint(camera, _parentCorners[0]);
            Vector2 max = RectTransformUtility.WorldToScreenPoint(camera, _parentCorners[2]);
            screenRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

            return screenRect.width > 0f && screenRect.height > 0f;
        }
    }
}
