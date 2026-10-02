using UnityEngine;

namespace SombraStudios.Shared.UI.SafeArea
{
    /// <summary>
    /// Safe area math shared by the uGUI and UI Toolkit adapters. Pure functions, no Unity lifecycle.
    /// </summary>
    public static class SafeAreaCalculator
    {
        /// <summary>
        /// Calculates how far each edge of <paramref name="container"/> must move inwards to sit inside
        /// <paramref name="safeArea"/>. Edges already inside the safe area get an inset of zero.
        /// </summary>
        /// <param name="safeArea">
        /// The safe area, in the same y-up space as the container (e.g. <see cref="Screen.safeArea"/>).
        /// </param>
        /// <param name="container">The rect being fitted, in the same space as the safe area.</param>
        /// <param name="edges">Edges that receive an inset. Excluded edges return zero.</param>
        /// <param name="isHorizontallySymmetric">Use the larger of the left and right insets on both sides.</param>
        public static SafeAreaInsets CalculateInsets(Rect safeArea, Rect container, SafeAreaEdges edges,
            bool isHorizontallySymmetric = false)
        {
            float width = Mathf.Max(0f, container.width);
            float height = Mathf.Max(0f, container.height);

            float left = Mathf.Clamp(safeArea.xMin - container.xMin, 0f, width);
            float right = Mathf.Clamp(container.xMax - safeArea.xMax, 0f, width);
            float top = Mathf.Clamp(container.yMax - safeArea.yMax, 0f, height);
            float bottom = Mathf.Clamp(safeArea.yMin - container.yMin, 0f, height);

            // In landscape the notch sits on one side only; mirroring it keeps centred layouts centred.
            if (isHorizontallySymmetric)
            {
                left = right = Mathf.Max(left, right);
            }

            return new SafeAreaInsets(
                HasEdge(edges, SafeAreaEdges.Left) ? left : 0f,
                HasEdge(edges, SafeAreaEdges.Right) ? right : 0f,
                HasEdge(edges, SafeAreaEdges.Top) ? top : 0f,
                HasEdge(edges, SafeAreaEdges.Bottom) ? bottom : 0f);
        }

        /// <summary>
        /// Converts insets into normalized anchors for a RectTransform stretched over a parent of
        /// <paramref name="containerSize"/>. A non-positive size returns a full stretch.
        /// </summary>
        public static void CalculateAnchors(SafeAreaInsets insets, Vector2 containerSize,
            out Vector2 anchorMin, out Vector2 anchorMax)
        {
            if (containerSize.x <= 0f || containerSize.y <= 0f)
            {
                anchorMin = Vector2.zero;
                anchorMax = Vector2.one;
                return;
            }

            anchorMin = new Vector2(insets.Left / containerSize.x, insets.Bottom / containerSize.y);
            anchorMax = new Vector2(1f - insets.Right / containerSize.x, 1f - insets.Top / containerSize.y);

            // A container lying entirely in the unsafe region (e.g. under the notch) would otherwise invert.
            anchorMax = Vector2.Max(anchorMin, anchorMax);
        }

        /// <summary>
        /// Whether <paramref name="edges"/> contains any bit of <paramref name="edge"/>.
        /// </summary>
        public static bool HasEdge(SafeAreaEdges edges, SafeAreaEdges edge)
        {
            // Bitwise instead of Enum.HasFlag, which boxes on Unity's Mono runtime.
            return (edges & edge) != 0;
        }
    }
}
