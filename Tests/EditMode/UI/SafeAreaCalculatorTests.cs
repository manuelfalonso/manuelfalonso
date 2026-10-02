using NUnit.Framework;
using SombraStudios.Shared.UI.SafeArea;
using UnityEngine;

namespace SombraStudios.Shared.Tests.UI
{
    public class SafeAreaCalculatorTests
    {
        private const float Tolerance = 1e-5f;

        // A 1000x2000 portrait screen with a 100 px notch on top and a 50 px home indicator at the bottom.
        private static readonly Rect ScreenRect = new Rect(0f, 0f, 1000f, 2000f);
        private static readonly Rect PortraitSafeArea = Rect.MinMaxRect(0f, 50f, 1000f, 1900f);

        [Test]
        public void CalculateInsets_SafeAreaMatchesContainer_ReturnsZero()
        {
            var insets = SafeAreaCalculator.CalculateInsets(ScreenRect, ScreenRect, SafeAreaEdges.All);

            Assert.AreEqual(SafeAreaInsets.Zero, insets);
        }

        [Test]
        public void CalculateInsets_PortraitNotch_InsetsTopAndBottom()
        {
            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, ScreenRect, SafeAreaEdges.All);

            Assert.AreEqual(new SafeAreaInsets(left: 0f, right: 0f, top: 100f, bottom: 50f), insets);
        }

        [Test]
        public void CalculateInsets_ExcludedEdges_ReturnZero()
        {
            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, ScreenRect, SafeAreaEdges.Top);

            Assert.AreEqual(new SafeAreaInsets(left: 0f, right: 0f, top: 100f, bottom: 0f), insets);
        }

        [Test]
        public void CalculateInsets_NoEdges_ReturnsZero()
        {
            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, ScreenRect, SafeAreaEdges.None);

            Assert.AreEqual(SafeAreaInsets.Zero, insets);
        }

        [Test]
        public void CalculateInsets_LandscapeNotchOnOneSide_KeepsSidesAsymmetric()
        {
            var screen = new Rect(0f, 0f, 2000f, 1000f);
            var safeArea = Rect.MinMaxRect(80f, 0f, 1980f, 1000f);

            var insets = SafeAreaCalculator.CalculateInsets(safeArea, screen, SafeAreaEdges.All);

            Assert.AreEqual(new SafeAreaInsets(left: 80f, right: 20f, top: 0f, bottom: 0f), insets);
        }

        [Test]
        public void CalculateInsets_HorizontallySymmetric_UsesLargerSideOnBoth()
        {
            var screen = new Rect(0f, 0f, 2000f, 1000f);
            var safeArea = Rect.MinMaxRect(80f, 0f, 1980f, 1000f);

            var insets = SafeAreaCalculator.CalculateInsets(safeArea, screen, SafeAreaEdges.All,
                isHorizontallySymmetric: true);

            Assert.AreEqual(new SafeAreaInsets(left: 80f, right: 80f, top: 0f, bottom: 0f), insets);
        }

        [Test]
        public void CalculateInsets_SymmetricWithOneSideExcluded_KeepsItZero()
        {
            var screen = new Rect(0f, 0f, 2000f, 1000f);
            var safeArea = Rect.MinMaxRect(80f, 0f, 1980f, 1000f);

            var insets = SafeAreaCalculator.CalculateInsets(safeArea, screen, SafeAreaEdges.Right,
                isHorizontallySymmetric: true);

            Assert.AreEqual(new SafeAreaInsets(left: 0f, right: 80f, top: 0f, bottom: 0f), insets);
        }

        [Test]
        public void CalculateInsets_ContainerAwayFromScreenOrigin_IsRelativeToContainer()
        {
            // A 200 px top bar: only the notch reaches it, the home indicator does not.
            var topBar = new Rect(0f, 1800f, 1000f, 200f);

            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, topBar, SafeAreaEdges.All);

            Assert.AreEqual(new SafeAreaInsets(left: 0f, right: 0f, top: 100f, bottom: 0f), insets);
        }

        [Test]
        public void CalculateInsets_ContainerInsideSafeArea_ReturnsZero()
        {
            var centre = new Rect(100f, 500f, 800f, 1000f);

            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, centre, SafeAreaEdges.All);

            Assert.AreEqual(SafeAreaInsets.Zero, insets);
        }

        [Test]
        public void CalculateInsets_ContainerFullyUnderNotch_ClampsToContainerSize()
        {
            var underNotch = new Rect(0f, 1950f, 1000f, 50f);

            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, underNotch, SafeAreaEdges.All);

            Assert.AreEqual(50f, insets.Top);
            Assert.AreEqual(0f, insets.Bottom);
        }

        [Test]
        public void CalculateAnchors_ZeroInsets_StretchesOverParent()
        {
            // Regression for the old UI.Mobile.SafeArea, which multiplied position by size and collapsed
            // the rect to zero whenever the safe area started at the screen origin.
            SafeAreaCalculator.CalculateAnchors(SafeAreaInsets.Zero, ScreenRect.size,
                out Vector2 anchorMin, out Vector2 anchorMax);

            AssertVector(Vector2.zero, anchorMin);
            AssertVector(Vector2.one, anchorMax);
        }

        [Test]
        public void CalculateAnchors_Insets_NormalizesByContainerSize()
        {
            var insets = new SafeAreaInsets(left: 100f, right: 50f, top: 200f, bottom: 100f);

            SafeAreaCalculator.CalculateAnchors(insets, ScreenRect.size, out Vector2 anchorMin, out Vector2 anchorMax);

            AssertVector(new Vector2(0.1f, 0.05f), anchorMin);
            AssertVector(new Vector2(0.95f, 0.9f), anchorMax);
        }

        [Test]
        public void CalculateAnchors_InsetsLargerThanContainer_NeverInverts()
        {
            var insets = new SafeAreaInsets(left: 800f, right: 800f, top: 50f, bottom: 0f);

            SafeAreaCalculator.CalculateAnchors(insets, new Vector2(1000f, 50f),
                out Vector2 anchorMin, out Vector2 anchorMax);

            Assert.That(anchorMax.x, Is.GreaterThanOrEqualTo(anchorMin.x));
            Assert.That(anchorMax.y, Is.GreaterThanOrEqualTo(anchorMin.y));
        }

        [Test]
        public void CalculateAnchors_ZeroSizeContainer_StretchesOverParent()
        {
            var insets = new SafeAreaInsets(left: 10f, right: 10f, top: 10f, bottom: 10f);

            SafeAreaCalculator.CalculateAnchors(insets, Vector2.zero, out Vector2 anchorMin, out Vector2 anchorMax);

            AssertVector(Vector2.zero, anchorMin);
            AssertVector(Vector2.one, anchorMax);
        }

        [Test]
        public void PortraitNotch_EndToEnd_ProducesExpectedAnchors()
        {
            var insets = SafeAreaCalculator.CalculateInsets(PortraitSafeArea, ScreenRect, SafeAreaEdges.All);

            SafeAreaCalculator.CalculateAnchors(insets, ScreenRect.size, out Vector2 anchorMin, out Vector2 anchorMax);

            AssertVector(new Vector2(0f, 0.025f), anchorMin);
            AssertVector(new Vector2(1f, 0.95f), anchorMax);
        }

        private static void AssertVector(Vector2 expected, Vector2 actual)
        {
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(Tolerance), "x");
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(Tolerance), "y");
        }
    }
}
