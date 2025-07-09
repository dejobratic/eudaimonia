using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Tests.Unit.Kernel
{
    public class EntityTests
    {
        private class Entity(int key) : Entity<int>(key)
        {
        }

        [Fact]
        public void Constructor_WithDefaultValueId_ThrowsException()
        {
            static Entity Action() => new(0);

            var exception = Assert.Throws<ArgumentException>(Action);
            Assert.Equal("A default value cannot be used as Entity Id. (Parameter 'id')", exception.Message);
        }

        [Fact]
        public void Equality_WhenEntitiesHaveSameKeys_AreEqual()
        {
            var a = new Entity(1);
            var b = new Entity(1);

            AssertAreEqual(a, b);
        }

        [Fact]
        public void Equality_WhenEntitiesHaveDifferentKeys_AreNotEqual()
        {
            var a = new Entity(1);
            var b = new Entity(2);

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
            var a = new Entity(1);

            AssertAreNotEqual(a, null);
        }

        [Fact]
        public void GetHashCode_WhenEntitiesHaveSameHashCode_AreEqual()
        {
            var a = new Entity(1);
            var b = new Entity(1);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenEntitiesHaveDifferentHashCode_AreNotEqual()
        {
            var a = new Entity(1);
            var b = new Entity(2);

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