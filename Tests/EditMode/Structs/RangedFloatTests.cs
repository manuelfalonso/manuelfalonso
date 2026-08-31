using NUnit.Framework;
using SombraStudios.Shared.Structs;
using UnityEngine;

namespace SombraStudios.Shared.Tests.Structs
{
    public class RangedFloatTests
    {
        private const int SampleCount = 1000;

        private Random.State _randomState;

        [SetUp]
        public void SetUp()
        {
            // Unity's random generator is global; snapshot it so these tests neither
            // flake on nor leak into whatever else runs in this session.
            _randomState = Random.state;
            Random.InitState(12345);
        }

        [TearDown]
        public void TearDown()
        {
            Random.state = _randomState;
        }

        [Test]
        public void GetRandom_StaysWithinBounds()
        {
            var range = new RangedFloat(-2.5f, 7.5f);

            for (var i = 0; i < SampleCount; i++)
            {
                var value = range.GetRandom();
                Assert.That(value, Is.InRange(range.MinValue, range.MaxValue));
            }
        }

        [Test]
        public void GetRandom_EqualBounds_ReturnsThatValue()
        {
            var range = new RangedFloat(3f, 3f);

            Assert.AreEqual(3f, range.GetRandom());
        }

        // Documents the behaviour the library relies on rather than guards against:
        // reversed bounds are accepted and yield values inside the same interval.
        [Test]
        public void GetRandom_ReversedBounds_StaysWithinBounds()
        {
            var range = new RangedFloat(7.5f, -2.5f);

            for (var i = 0; i < SampleCount; i++)
            {
                var value = range.GetRandom();
                Assert.That(value, Is.InRange(-2.5f, 7.5f));
            }
        }

        [Test]
        public void Equals_SameBounds_IsTrue()
        {
            var a = new RangedFloat(1f, 2f);
            var b = new RangedFloat(1f, 2f);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equals_DifferentBounds_IsFalse()
        {
            var a = new RangedFloat(1f, 2f);
            var b = new RangedFloat(1f, 3f);

            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        // GetRandom uses float.Equals, not ==, so NaN bounds compare equal to
        // themselves and stay consistent with GetHashCode.
        [Test]
        public void Equals_NaNBounds_IsTrueAndHashesMatch()
        {
            var a = new RangedFloat(float.NaN, float.NaN);
            var b = new RangedFloat(float.NaN, float.NaN);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equals_BoxedAndOtherType_BehavesAsExpected()
        {
            var range = new RangedFloat(1f, 2f);

            Assert.IsTrue(range.Equals((object)new RangedFloat(1f, 2f)));
            Assert.IsFalse(range.Equals("not a range"));
        }
    }
}