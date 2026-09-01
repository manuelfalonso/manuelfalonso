using System.Collections.Generic;
using NUnit.Framework;
using SombraStudios.Shared.AI;
using UnityEngine;
using UnityEngine.TestTools;

namespace SombraStudios.Shared.Tests.AI
{
    public class LineOfSightTests
    {
        // "Player" is one of Unity's built-in tags, so these tests do not depend on
        // project-specific tag setup. CompareTag with an undefined tag would error.
        private const string IgnoredTag = "Player";

        // Edit Mode tests run in whichever scene the Editor has open, so everything is built far
        // from the origin: a ~0 layer mask would otherwise pick up that scene's own colliders.
        private static readonly Vector3 FarFromAnything = new Vector3(10000f, 10000f, 10000f);

        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var go in _spawned)
            {
                if (go != null) { Object.DestroyImmediate(go); }
            }

            _spawned.Clear();
        }

        #region IsInFieldOfView

        [Test]
        public void IsInFieldOfView_TargetStraightAhead_IsTrue()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.forward * 5f);

            Assert.IsTrue(LineOfSight.IsInFieldOfView(observer, target, 90f));
        }

        [Test]
        public void IsInFieldOfView_TargetBehind_IsFalse()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.back * 5f);

            Assert.IsFalse(LineOfSight.IsInFieldOfView(observer, target, 90f));
        }

        // viewAngle is the full cone, so 90 degrees reaches 45 either side of forward.
        // These two cases sit deliberately either side of that boundary.
        [Test]
        public void IsInFieldOfView_TreatsAngleAsFullCone()
        {
            var observer = NewObject("observer", Vector3.zero);
            var inside = NewObject("inside", Quaternion.Euler(0f, 40f, 0f) * Vector3.forward);
            var outside = NewObject("outside", Quaternion.Euler(0f, 50f, 0f) * Vector3.forward);

            Assert.IsTrue(LineOfSight.IsInFieldOfView(observer, inside, 90f), "40 degrees off forward should be inside a 90 degree cone.");
            Assert.IsFalse(LineOfSight.IsInFieldOfView(observer, outside, 90f), "50 degrees off forward should be outside a 90 degree cone.");
        }

        // In 2D the observer's local up is treated as forward.
        [Test]
        public void IsInFieldOfView_Is2D_UsesUpAxis()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.up * 5f);

            Assert.IsTrue(LineOfSight.IsInFieldOfView(observer, target, 90f, is2D: true));
            Assert.IsFalse(LineOfSight.IsInFieldOfView(observer, target, 90f, is2D: false));
        }

        [Test]
        public void IsInFieldOfView_NullArgument_IsFalseAndLogs()
        {
            var observer = NewObject("observer", Vector3.zero);

            LogAssert.Expect(LogType.Error, "Target or entity is null");
            Assert.IsFalse(LineOfSight.IsInFieldOfView(observer, null, 90f));
        }

        #endregion

        #region IsInFieldOfView (vector overload)

        // No Transforms, no scene: the overload exists so the angle math can be tested directly.
        [Test]
        public void IsInFieldOfViewVectors_TreatsAngleAsFullCone()
        {
            var forward = Vector3.forward;

            Assert.IsTrue(LineOfSight.IsInFieldOfView(Vector3.zero, forward, Quaternion.Euler(0f, 40f, 0f) * Vector3.forward, 90f));
            Assert.IsFalse(LineOfSight.IsInFieldOfView(Vector3.zero, forward, Quaternion.Euler(0f, 50f, 0f) * Vector3.forward, 90f));
        }

        [Test]
        public void IsInFieldOfViewVectors_UnnormalizedForward_BehavesTheSame()
        {
            var target = Vector3.forward * 5f;

            Assert.IsTrue(LineOfSight.IsInFieldOfView(Vector3.zero, Vector3.forward * 37f, target, 90f));
        }

        [Test]
        public void IsInFieldOfViewVectors_OriginOffset_IsRelativeToOrigin()
        {
            var origin = new Vector3(100f, 0f, 0f);

            Assert.IsTrue(LineOfSight.IsInFieldOfView(origin, Vector3.forward, origin + Vector3.forward, 90f));
            Assert.IsFalse(LineOfSight.IsInFieldOfView(origin, Vector3.forward, Vector3.zero, 90f), "The world origin is off to the side from this observer.");
        }

        // Documented behaviour rather than desired behaviour: Vector3.Angle returns 0 for a
        // zero-length vector, so these degenerate inputs report "in view".
        [Test]
        public void IsInFieldOfViewVectors_DegenerateInput_ReportsTrue()
        {
            Assert.IsTrue(LineOfSight.IsInFieldOfView(Vector3.zero, Vector3.zero, Vector3.forward, 90f), "A zero facing direction reports in-view.");
            Assert.IsTrue(LineOfSight.IsInFieldOfView(Vector3.zero, Vector3.forward, Vector3.zero, 90f), "A target at the observer's own position reports in-view.");
        }

        // The Transform overload must delegate, not diverge.
        [Test]
        public void IsInFieldOfView_TransformAndVectorOverloadsAgree()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Quaternion.Euler(0f, 40f, 0f) * Vector3.forward * 5f);

            var viaTransforms = LineOfSight.IsInFieldOfView(observer, target, 90f);
            var viaVectors = LineOfSight.IsInFieldOfView(observer.position, observer.forward, target.position, 90f);

            Assert.AreEqual(viaTransforms, viaVectors);
        }

        #endregion

        #region IsInSight (3D)

        [Test]
        public void IsInSight_NothingBetween_IsTrue()
        {
            var data = Sight(NewObject("observer", Vector3.zero), NewObject("target", Vector3.forward * 10f));

            Assert.IsTrue(LineOfSight.IsInSight(data, out _));
        }

        [Test]
        public void IsInSight_WallBetween_IsFalse()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.forward * 10f);
            var wall = NewCollider("wall", Vector3.forward * 5f);

            Assert.IsFalse(LineOfSight.IsInSight(Sight(observer, target), out var hit));
            Assert.AreEqual(wall.transform, hit.collider.transform);
        }

        // The target's own collider is the nearest hit, and must not read as an obstruction.
        [Test]
        public void IsInSight_TargetItselfIsTheHit_IsTrue()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewCollider("target", Vector3.forward * 10f);

            Assert.IsTrue(LineOfSight.IsInSight(Sight(observer, target.transform), out _));
        }

        // The inverted TagToIgnore behaviour: a tagged collider does not block the line.
        [Test]
        public void IsInSight_BlockerHasIgnoredTag_IsTrue()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.forward * 10f);
            var wall = NewCollider("wall", Vector3.forward * 5f);
            wall.tag = IgnoredTag;

            var data = Sight(observer, target, IgnoredTag);

            Assert.IsTrue(LineOfSight.IsInSight(data, out _), "A collider carrying TagToIgnore must not obstruct the line.");
        }

        [Test]
        public void IsInSight_BlockerHasDifferentTag_IsFalse()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.forward * 10f);
            NewCollider("wall", Vector3.forward * 5f);

            var data = Sight(observer, target, IgnoredTag);

            Assert.IsFalse(LineOfSight.IsInSight(data, out _));
        }

        [Test]
        public void IsInSight_ObstacleBeyondTheTarget_IsTrue()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.forward * 5f);
            NewCollider("wall", Vector3.forward * 20f);

            Assert.IsTrue(LineOfSight.IsInSight(Sight(observer, target), out _), "A collider past the target is outside the ray's length.");
        }

        [Test]
        public void IsInSight_NullPoint_IsFalse()
        {
            var data = Sight(NewObject("observer", Vector3.zero), null);

            Assert.IsFalse(LineOfSight.IsInSight(data, out _));
        }

        #endregion

        #region IsInSight2D

        [Test]
        public void IsInSight2D_NothingBetween_IsTrue()
        {
            var data = Sight(NewObject("observer", Vector3.zero), NewObject("target", Vector3.right * 10f));

            Assert.IsTrue(LineOfSight.IsInSight2D(data, out _));
        }

        // The whole point of the 2D path: a 2D collider must be seen, which the 3D
        // raycast could never report.
        [Test]
        public void IsInSight2D_Collider2DBetween_IsFalse()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.right * 10f);
            var wall = NewCollider2D("wall", Vector3.right * 5f);

            Assert.IsFalse(LineOfSight.IsInSight2D(Sight(observer, target), out var hit));
            Assert.AreEqual(wall.transform, hit.collider.transform);
        }

        [Test]
        public void IsInSight2D_IgnoresCollider3D()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.right * 10f);
            NewCollider("wall3D", Vector3.right * 5f);

            Assert.IsTrue(LineOfSight.IsInSight2D(Sight(observer, target), out _), "A 3D collider is invisible to the 2D physics system.");
        }

        [Test]
        public void IsInSight2D_BlockerHasIgnoredTag_IsTrue()
        {
            var observer = NewObject("observer", Vector3.zero);
            var target = NewObject("target", Vector3.right * 10f);
            var wall = NewCollider2D("wall", Vector3.right * 5f);
            wall.tag = IgnoredTag;

            var data = Sight(observer, target, IgnoredTag);

            Assert.IsTrue(LineOfSight.IsInSight2D(data, out _));
        }

        #endregion

        #region Combined

        [Test]
        public void IsInFieldOfViewAndInSight_InsideConeAndUnobstructed_IsTrue()
        {
            var data = Sight(NewObject("observer", Vector3.zero), NewObject("target", Vector3.forward * 10f));

            Assert.IsTrue(LineOfSight.IsInFieldOfViewAndInSight(data, 90f, out _));
        }

        [Test]
        public void IsInFieldOfViewAndInSight_OutsideCone_IsFalse()
        {
            var data = Sight(NewObject("observer", Vector3.zero), NewObject("target", Vector3.back * 10f));

            Assert.IsFalse(LineOfSight.IsInFieldOfViewAndInSight(data, 90f, out _));
        }

        [Test]
        public void IsInFieldOfViewAndInSight_NonPositiveAngle_IsFalseAndLogs()
        {
            var data = Sight(NewObject("observer", Vector3.zero), NewObject("target", Vector3.forward * 10f));

            LogAssert.Expect(LogType.Error, "ViewAngle must be greater than zero");
            Assert.IsFalse(LineOfSight.IsInFieldOfViewAndInSight(data, 0f, out _));
        }

        [Test]
        public void IsInFieldOfViewAndInSight_NullObserver_IsFalseAndLogs()
        {
            var data = Sight(null, NewObject("target", Vector3.forward * 10f));

            LogAssert.Expect(LogType.Error, "StartPoint (the observer) is null");
            Assert.IsFalse(LineOfSight.IsInFieldOfViewAndInSight(data, 90f, out _));
        }

        #endregion

        #region Helpers

        private static LineOfSight.IsInSightData Sight(Transform start, Transform end, string tagToIgnore = null)
        {
            return new LineOfSight.IsInSightData(start, end, ~0, tagToIgnore: tagToIgnore);
        }

        private Transform NewObject(string name, Vector3 position)
        {
            var go = new GameObject(name);
            _spawned.Add(go);
            go.transform.position = FarFromAnything + position;
            return go.transform;
        }

        private GameObject NewCollider(string name, Vector3 position)
        {
            var go = new GameObject(name);
            _spawned.Add(go);
            go.transform.position = FarFromAnything + position;
            go.AddComponent<BoxCollider>();
            // Edit Mode does not step physics, so collider transforms must be pushed to the
            // physics scene by hand before a query can see them.
            Physics.SyncTransforms();
            return go;
        }

        private GameObject NewCollider2D(string name, Vector3 position)
        {
            var go = new GameObject(name);
            _spawned.Add(go);
            go.transform.position = FarFromAnything + position;
            go.AddComponent<BoxCollider2D>();
            Physics2D.SyncTransforms();
            return go;
        }

        #endregion
    }
}
