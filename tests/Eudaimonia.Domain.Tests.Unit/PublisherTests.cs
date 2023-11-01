using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class PublisherTests
{
    private static readonly PublisherId Id = new();
    private static readonly Text FullName = new("HarperCollins");
    private static readonly Text Bio = new("HarperCollins Publishers LLC is one of the world's largest publishing companies.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var publisher = new Publisher(Id, FullName, Bio);

        Assert.NotNull(publisher);
        Assert.Equal(Id, publisher.Id);
        Assert.Equal(FullName, publisher.FullName);
        Assert.Equal(Bio, publisher.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Publisher action() => new(Id, null!, Bio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Publisher with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var publisher = new Publisher(Id, FullName, null);

        Assert.Equal(Id, publisher.Id);
        Assert.Equal(FullName, publisher.FullName);
        Assert.Null(publisher.Bio);
    }
}