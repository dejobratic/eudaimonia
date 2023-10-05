using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class GuidIdTests
{
    private class EntityId : GuidId { }

    [Fact]
    public void Constructor_Default_CreatesInstance()
    {
        var id = new EntityId();

        id.Should().NotBeNull();
        id.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WhenInvokedMultipleTimes_CreatesUniqueInstances()
    {
        var id1 = new EntityId();
        var id2 = new EntityId();

        id1.Should().NotBeEquivalentTo(id2);
    }
}