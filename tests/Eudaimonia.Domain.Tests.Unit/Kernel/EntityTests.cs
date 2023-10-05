using Eudaimonia.Domain.Kernel;

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
            static Entity action() => new(0, "a");

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("A default value cannot be used as Entity Id. (Parameter 'id')", exception.Message);
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

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenEntitiesHaveDifferentHashCode_AreNotEqual()
        {
            var a = new Entity(1, "a");
            var b = new Entity(2, "a");

            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

        private static void AssertAreEqual(Entity? a, Entity? b)
        {
            Assert.Equal(a, b);
            Assert.Equal(b, a);

            Assert.True(a == b);
            Assert.True(b == a);

            Assert.False(a != b);
            Assert.False(b != a);
        }

        private static void AssertAreNotEqual(Entity? a, Entity? b)
        {
            Assert.NotEqual(a, b);
            Assert.NotEqual(b, a);

            Assert.False(a == b);
            Assert.False(b == a);

            Assert.True(a != b);
            Assert.True(b != a);
        }
    }
}