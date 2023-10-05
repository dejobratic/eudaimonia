namespace Eudaimonia.Domain.Tests.Unit;

public class GuidIdTests
{
    private class EntityId : GuidId
    { }

    [Fact]
    public void Constructor_Default_CreatesInstance()
    {
        var id = new EntityId();

        Assert.NotNull(id);
        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void Constructor_WhenInvokedMultipleTimes_CreatesUniqueInstances()
    {
        var id1 = new EntityId();
        var id2 = new EntityId();

        Assert.NotEqual(id1, id2);
        Assert.NotEqual(id1.Value, id2.Value);
    }
}