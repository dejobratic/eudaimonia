using Eudaimonia.Domain.Kernel;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Eudaimonia.Domain.Tests.Unit.Kernel
{
    public class EntityTests
    {
        private class Entity : Entity<int>
        {
            public string Property { get; }

            public Entity(int key, string property) : base(key)
            {
                Property = property;
            }
        }

        [Fact]
        public void Constructor_WithDefaultValueId_ThrowsException()
        {
            var action = () => new Entity(0, "a");

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Equality_WhenEntitiesHaveSameKeys_AreEqual()
        {
            var a = new Entity(1, "a");
            var b = new Entity(1, "b");

            AssertAreEqual(a, b);
        }

        [Fact]
        public void Equality_WhenEntitiesHaveDifferentKeys_AreNotEqual()
        {
            var a = new Entity(1, "a");
            var b = new Entity(2, "a");

            AssertAreNotEqual(a, b);
        }

        [Fact]
        public void Equality_WhenEntitiesAreNull_AreEqual()
        {
            Entity? a = null;
            Entity? b = null;

            AssertAreEqual(a, b);
        }

        [Fact]
        public void Equality_WhenEntityIsNull_AreNotEqual()
        {
            var a = new Entity(1, "a");

            AssertAreNotEqual(a, null);
        }

        [Fact]
        public void GetHashCode_WhenEntitiesHaveSameHashCode_AreEqual()
        {
            var a = new Entity(1, "a");
            var b = new Entity(1, "b");

            a.GetHashCode().Should().Be(b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenEntitiesHaveDifferentHashCode_AreNotEqual()
        {
            var a = new Entity(1, "a");
            var b = new Entity(2, "a");

            a.GetHashCode().Should().NotBe(b.GetHashCode());
        }

        private static void AssertAreEqual(Entity? a, Entity? b)
        {
            using var scope = new AssertionScope();

            a.Should().Be(b);
            b.Should().Be(a);

            (a == b).Should().BeTrue();
            (b == a).Should().BeTrue();

            (a != b).Should().BeFalse();
            (b != a).Should().BeFalse();
        }

        private static void AssertAreNotEqual(Entity? a, Entity? b)
        {
            using var scope = new AssertionScope();

            a.Should().NotBe(b);
            b.Should().NotBe(a);

            (a == b).Should().BeFalse();
            (b == a).Should().BeFalse();

            (a != b).Should().BeTrue();
            (b != a).Should().BeTrue();
        }
    }
}