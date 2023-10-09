using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class PublisherTests
{
    private static readonly Text PublisherFullName = new("HarperCollins");
    private static readonly Text PublisherBio = new("HarperCollins Publishers LLC is one of the world's largest publishing companies.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var publisher = new Publisher(PublisherFullName, PublisherBio);

        Assert.NotNull(publisher.Id);
        Assert.Equal(PublisherFullName, publisher.FullName);
        Assert.Equal(PublisherBio, publisher.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Publisher action() => new(null!, PublisherBio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Publisher with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var publisher = new Publisher(PublisherFullName, null);

        Assert.NotNull(publisher.Id);
        Assert.Equal(PublisherFullName, publisher.FullName);
        Assert.Null(publisher.Bio);
    }
}