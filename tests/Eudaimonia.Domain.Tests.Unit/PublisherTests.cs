using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class PublisherTests
{
    private static readonly Text PublisherFullName = new("HarperCollins");
    private static readonly Text PublisherBio = new("HarperCollins Publishers LLC is one of the world's largest publishing companies.");
    private static readonly BookId[] PublishedBookIds = new[] { new BookId(), new BookId() };

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var publisher = new Publisher(PublisherFullName, PublisherBio, PublishedBookIds);

        Assert.NotNull(publisher.Id);
        Assert.Equal(PublisherFullName, publisher.FullName);
        Assert.Equal(PublisherBio, publisher.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Publisher action() => new(null!, PublisherBio, PublishedBookIds);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Publisher with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var publisher = new Publisher(PublisherFullName, null, PublishedBookIds);

        Assert.NotNull(publisher.Id);
        Assert.Equal(PublisherFullName, publisher.FullName);
        Assert.Null(publisher.Bio);
    }

    [Fact]
    public void Constructor_WhenPublisheredBookIdsIsNull_ThrowsException()
    {
        static Publisher action() => new(PublisherFullName, PublisherBio, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Publisher with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublishedBookIds", "At least one BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAtLeastOnePublisheredBookIdIsNotProvided_ThrowsException()
    {
        static Publisher action() => new(PublisherFullName, PublisherBio, Array.Empty<BookId>());

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Publisher with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("PublishedBookIds", "At least one BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenWhenMultipleSamePublishedBookIdsAreProvided_CreatesInstanceWithUniquePublishedBookIds()
    {
        var publisher = new Publisher(PublisherFullName, PublisherBio, new[] { PublishedBookIds[0], PublishedBookIds[1], PublishedBookIds[1] });

        Assert.NotNull(publisher.Id);
        Assert.Equal(PublisherFullName, publisher.FullName);
        Assert.Equal(PublisherBio, publisher.Bio);
        Assert.Equal(2, publisher.PublishedBookIds.Count());
        Assert.Equivalent(PublishedBookIds, publisher.PublishedBookIds);
    }
}