using NUnit.Framework;
using SombraStudios.Shared.Structs;
using UnityEngine;

namespace SombraStudios.Shared.Tests.Structs
{
    public class RangedIntTests
    {
        private const int SampleCount = 1000;

        private Random.State _randomState;

        [SetUp]
        public void SetUp()
        {
            _randomState = Random.state;
            Random.InitState(12345);
        }

        [TearDown]
        public void TearDown()
        {
            Random.state = _randomState;
        }

        [Test]
        public void GetRandom_StaysWithinInclusiveBounds()
        {
            var range = new RangedInt(-3, 4);

            for (var i = 0; i < SampleCount; i++)
            {
                var value = range.GetRandom();
                Assert.That(value, Is.InRange(range.MinValue, range.MaxValue));
            }
        }

        // Pins the MaxValue + 1 in GetRandom: Random.Range's int overload has an
        // exclusive upper bound, so the +1 is what makes MaxValue reachable.
        // An off-by-one either way breaks exactly this test.
        [Test]
        public void GetRandom_ReachesBothBounds()
        {
            var range = new RangedInt(1, 3);
            var seen = new bool[3];

            for (var i = 0; i < SampleCount; i++)
            {
                seen[range.GetRandom() - 1] = true;
            }

            Assert.IsTrue(seen[0], "MinValue was never returned.");
            Assert.IsTrue(seen[2], "MaxValue was never returned - the inclusive upper bound is broken.");
        }

        [Test]
        public void GetRandom_EqualBounds_ReturnsThatValue()
        {
            var range = new RangedInt(5, 5);

            for (var i = 0; i < SampleCount; i++)
            {
                Assert.AreEqual(5, range.GetRandom());
            }
        }

        // Documents the behaviour the library relies on rather than guards against.
        [Test]
        public void GetRandom_ReversedBounds_StaysWithinBounds()
        {
            var range = new RangedInt(4, -3);

            for (var i = 0; i < SampleCount; i++)
            {
                var value = range.GetRandom();
                Assert.That(value, Is.InRange(-3, 4));
            }
        }

        [Test]
        public void Equals_SameBounds_IsTrue()
        {
            var a = new RangedInt(1, 2);
            var b = new RangedInt(1, 2);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equals_DifferentBounds_IsFalse()
        {
            var a = new RangedInt(1, 2);
            var b = new RangedInt(2, 1);

            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        [Test]
        public void Equals_BoxedAndOtherType_BehavesAsExpected()
        {
            var range = new RangedInt(1, 2);

            Assert.IsTrue(range.Equals((object)new RangedInt(1, 2)));
            Assert.IsFalse(range.Equals(5));
        }
    }
}