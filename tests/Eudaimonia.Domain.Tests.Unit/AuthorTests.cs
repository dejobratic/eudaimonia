using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class AuthorTests
{
    private static readonly AuthorId Id = new();
    private static readonly Text FullName = new("J.R.R. Tolkien");
    private static readonly Text Bio = new("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var author = new Author(Id, FullName, Bio);

        Assert.NotNull(author);
        Assert.Equal(Id, author.Id);
        Assert.Equal(FullName, author.FullName);
        Assert.Equal(Bio, author.Bio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        static Author action() => new(Id, null!, Bio);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Author with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("FullName", "FullName must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var author = new Author(Id, FullName, null);

        Assert.Equal(Id, author.Id);
        Assert.Equal(FullName, author.FullName);
        Assert.Null(author.Bio);
    }
}