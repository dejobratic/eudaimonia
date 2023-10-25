using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class GuidIdTests
{
    private class EntityId : GuidId
    {
        public EntityId() { }

        public EntityId(string value) : base(value) { }

        public EntityId(Guid value) : base(value) { }
    }

    [Fact]
    public void Constructor_Default_CreatesInstance()
    {
        var id = new EntityId();

        Assert.NotNull(id);
        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void Constructor_DefaultWhenInvokedMultipleTimes_CreatesUniqueInstances()
    {
        var id1 = new EntityId();
        var id2 = new EntityId();

        Assert.NotEqual(id1, id2);
        Assert.NotEqual(id1.Value, id2.Value);
    }

    [Fact]
    public void Constructor_WhenProvidingStringGuidValue_CreatesInstance()
    {
        var id = new EntityId("1d517b07-c14f-4211-be32-7b4cbf1a883a");

        Assert.NotNull(id);
        Assert.Equal(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"), id.Value);
    }

    [Fact]
    public void Constuctor_WhenProvidingInvalidStringGuidValue_ThrowsException()
    {
        static EntityId action() => new("invalid");

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for EntityId with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Value", "Value must be a valid non-empty Guid or Guid string.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenProvidingStringGuidValueAndInvokedMultipleTimes_CreatesSameInstance()
    {
        var id1 = new EntityId("1d517b07-c14f-4211-be32-7b4cbf1a883a");
        var id2 = new EntityId("1d517b07-c14f-4211-be32-7b4cbf1a883a");

        Assert.Equal(id1, id2);
        Assert.Equal(id1.Value, id2.Value);
    }

    [Fact]
    public void Constructor_WhenProvidingGuidValue_CreatesInstance()
    {
        var id = new EntityId(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"));

        Assert.NotNull(id);
        Assert.Equal(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"), id.Value);
    }

    [Fact]
    public void Constuctor_WhenProvidingEmptyGuidValue_ThrowsException()
    {
        static EntityId action() => new("00000000-0000-0000-0000-000000000000");

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for EntityId with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Value", "Value must be a valid non-empty Guid or Guid string.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenProvidingGuidValueAndInvokedMultipleTimes_CreatesSameInstance()
    {
        var id1 = new EntityId(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"));
        var id2 = new EntityId(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"));

        Assert.Equal(id1, id2);
        Assert.Equal(id1.Value, id2.Value);
    }

    [Fact]
    public void ToString_WhenInvoked_ReturnsStringRepresentation()
    {
        var id = new EntityId("1d517b07-c14f-4211-be32-7b4cbf1a883a");

        Assert.Equal("1d517b07-c14f-4211-be32-7b4cbf1a883a", id.ToString());
    }

    [Fact]
    public void ImplicitOperator_WhenInvoked_ReturnsGuidValue()
    {
        var id = new EntityId("1d517b07-c14f-4211-be32-7b4cbf1a883a");

        Guid guid = id;

        Assert.Equal(Guid.Parse("1d517b07-c14f-4211-be32-7b4cbf1a883a"), guid);
    }

    [Fact]
    public void ImplicitOperator_WhenInvokedOnNull_ReturnsEmptyGuid()
    {
        EntityId? id = null;

        Guid guid = id!;

        Assert.Equal(Guid.Empty, guid);
    }
}