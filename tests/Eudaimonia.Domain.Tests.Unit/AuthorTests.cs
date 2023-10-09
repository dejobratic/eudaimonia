using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class AuthorTests
{
    private static readonly Text AuthorFullName = new("J.R.R. Tolkien");
    private static readonly Text AuthorBio = new("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var author = new Author(AuthorFullName, AuthorBio);

        Assert.NotNull(author.Id);
        Assert.Equal(AuthorFullName, author.FullName);
        Assert.Equal(AuthorBio, author.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Author action() => new(null!, AuthorBio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Author with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var author = new Author(AuthorFullName, null);

        Assert.NotNull(author.Id);
        Assert.Equal(AuthorFullName, author.FullName);
        Assert.Null(author.Bio);
    }
}