using UnityEngine;
using UnityEngine.UIElements;

namespace SombraStudios.Shared.UI.SafeArea
{
    /// <summary>
    /// UI Toolkit container that keeps its children inside the device safe area (notch, punch-hole camera,
    /// home indicator) by setting its own padding.
    /// </summary>
    /// <remarks>
    /// Add it from the UI Builder Library (Project > Custom Controls) or in UXML as
    /// <c>&lt;SombraStudios.Shared.UI.SafeArea.SafeAreaElement edges="All" /&gt;</c>, usually stretched over the
    /// whole screen, and place the content that must stay visible and touchable inside it. A background set on
    /// the element itself still fills its full rect, under the padding.
    /// The element owns the padding of every edge it handles; edges excluded from <see cref="Edges"/> keep
    /// their USS or UXML padding. It only acts on runtime panels, so the UI Builder canvas is left untouched.
    /// </remarks>
    [UxmlElement]
    public partial class SafeAreaElement : VisualElement
    {
        /// <summary>
        /// USS class added to every safe area element.
        /// </summary>
        public static readonly string UssClassName = "safe-area";

        private SafeAreaEdges _edges = SafeAreaEdges.All;
        private bool _isHorizontallySymmetric;

        private IVisualElementScheduledItem _poll;
        private Rect _lastSafeArea;
        private Rect _lastBound;
        private Vector2Int _lastScreenSize;
        private bool _isDirty = true;

        /// <summary>
        /// Edges that are pushed inside the safe area.
        /// </summary>
        [UxmlAttribute]
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
        /// Use the larger of the left and right insets on both sides, so content stays centred in landscape.
        /// </summary>
        [UxmlAttribute]
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
        /// The padding applied on the last refresh, in panel units.
        /// </summary>
        public SafeAreaInsets CurrentInsets { get; private set; }

        /// <summary>
        /// Creates a safe area element.
        /// </summary>
        public SafeAreaElement()
        {
            AddToClassList(UssClassName);

            // A layout wrapper, like an empty RectTransform in uGUI: clicks on empty space reach what is behind.
            pickingMode = PickingMode.Ignore;

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        /// <summary>
        /// Recalculates and applies the safe area immediately, even if nothing seems to have changed.
        /// </summary>
        public void ForceRefresh()
        {
            _isDirty = true;
            Refresh();
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (!IsRuntimePanel(evt.destinationPanel))
            {
                return;
            }

            // There is no safe area changed event, and a safe area change does not always resize the panel.
            _poll ??= schedule.Execute(Refresh).Every(0);
            _poll.Resume();
            ForceRefresh();
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            _poll?.Pause();
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (!IsRuntimePanel(panel))
            {
                return;
            }

            Rect bound = worldBound;
            if (float.IsNaN(bound.width) || bound.width <= 0f || bound.height <= 0f)
            {
                return;
            }

            Rect safeArea = Screen.safeArea;
            var screenSize = new Vector2Int(Screen.width, Screen.height);
            if (!_isDirty && safeArea == _lastSafeArea && bound == _lastBound && screenSize == _lastScreenSize)
            {
                return;
            }

            // State is stored before the padding changes, which can re-enter through GeometryChangedEvent.
            _lastSafeArea = safeArea;
            _lastBound = bound;
            _lastScreenSize = screenSize;
            _isDirty = false;

            Rect panelSafeArea = ScreenToPanel(safeArea, screenSize.y);

            // Panel space is y-down while the calculator is y-up; flipping both rects keeps top and bottom right.
            CurrentInsets = SafeAreaCalculator.CalculateInsets(FlipY(panelSafeArea), FlipY(bound), _edges,
                _isHorizontallySymmetric);

            style.paddingLeft = ToPadding(SafeAreaEdges.Left, CurrentInsets.Left);
            style.paddingRight = ToPadding(SafeAreaEdges.Right, CurrentInsets.Right);
            style.paddingTop = ToPadding(SafeAreaEdges.Top, CurrentInsets.Top);
            style.paddingBottom = ToPadding(SafeAreaEdges.Bottom, CurrentInsets.Bottom);
        }

        private Rect ScreenToPanel(Rect screenRect, int screenHeight)
        {
            // Screen.safeArea has a bottom-left origin; RuntimePanelUtils expects a top-left one. It also applies
            // the PanelSettings scale mode, so no manual pixel-to-panel ratio is needed.
            Vector2 topLeft = RuntimePanelUtils.ScreenToPanel(panel,
                new Vector2(screenRect.xMin, screenHeight - screenRect.yMax));
            Vector2 bottomRight = RuntimePanelUtils.ScreenToPanel(panel,
                new Vector2(screenRect.xMax, screenHeight - screenRect.yMin));

            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        private StyleLength ToPadding(SafeAreaEdges edge, float inset)
        {
            // Null clears the inline value, handing excluded edges back to USS and UXML.
            return SafeAreaCalculator.HasEdge(_edges, edge)
                ? new StyleLength(inset)
                : new StyleLength(StyleKeyword.Null);
        }

        private static Rect FlipY(Rect rect)
        {
            return new Rect(rect.xMin, -rect.yMax, rect.width, rect.height);
        }

        private static bool IsRuntimePanel(IPanel targetPanel)
        {
            return targetPanel != null && targetPanel.contextType == ContextType.Player;
        }
    }
}
