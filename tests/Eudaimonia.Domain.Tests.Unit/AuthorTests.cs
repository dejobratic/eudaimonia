using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class AuthorTests
{
    private static readonly Text AuthorFullName = new("J.R.R. Tolkien");
    private static readonly Text AuthorBio = new("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");
    private static readonly BookId[] AuthoredBookIds = new[] { new BookId(), new BookId() };

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var author = new Author(AuthorFullName, AuthorBio, AuthoredBookIds);

        Assert.NotNull(author.Id);
        Assert.Equal(AuthorFullName, author.FullName);
        Assert.Equal(AuthorBio, author.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Author action() => new(null!, AuthorBio, AuthoredBookIds);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Author with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var author = new Author(AuthorFullName, null, AuthoredBookIds);

        Assert.NotNull(author.Id);
        Assert.Equal(AuthorFullName, author.FullName);
        Assert.Null(author.Bio);
    }

    [Fact]
    public void Constructor_WhenAuthoredBookIdsIsNull_ThrowsException()
    {
        static Author action() => new(AuthorFullName, AuthorBio, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Author with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("AuthoredBookIds", "At least one BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAtLeastOneAuthoredBookIdIsNotProvided_ThrowsException()
    {
        static Author action() => new(AuthorFullName, AuthorBio, Array.Empty<BookId>());

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Author with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("AuthoredBookIds", "At least one BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenWhenMultipleSameAuthoredBookIdsAreProvided_CreatesInstanceWithUniqueAuthoredBookIds()
    {
        var author = new Author(AuthorFullName, AuthorBio, new[] { AuthoredBookIds[0], AuthoredBookIds[1], AuthoredBookIds[1] });

        Assert.NotNull(author.Id);
        Assert.Equal(AuthorFullName, author.FullName);
        Assert.Equal(AuthorBio, author.Bio);
        Assert.Equal(2, author.AuthoredBookIds.Count());
        Assert.Equivalent(AuthoredBookIds, author.AuthoredBookIds);
    }
}