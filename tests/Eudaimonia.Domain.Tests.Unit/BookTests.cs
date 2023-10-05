using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookTests
{
    private static readonly Text HobbitTitle = new("The Hobbit");
    private static readonly Text HobbitDescription = new("The Hobbit");
    private static readonly Genre[] HobbitGenres = new[] { Genre.Fantasy };

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var book = new Book(HobbitTitle, HobbitDescription, HobbitGenres);

        Assert.NotNull(book.Id);
        Assert.Equal(HobbitTitle, book.Title);
        Assert.Equal(HobbitDescription, book.Description);
        Assert.Equivalent(book.Genres, HobbitGenres);
    }

    [Fact]
    public void Constructor_WhenTitleIsNull_ThrowsException()
    {
        static Book action() => new(null!, HobbitDescription, HobbitGenres);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Title", "Title must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenDescriptionIsNull_ThrowsException()
    {
        static Book action() => new(HobbitTitle, null!, HobbitGenres);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Description", "Description must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenGenreIsNull_ThrowsException()
    {
        static Book action() => new(HobbitTitle, HobbitDescription, null!);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Genres", "At least one Genre must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAtLeastOneGenreIsNotProvided_ThrowsException()
    {
        static Book action() => new(HobbitTitle, HobbitDescription, Array.Empty<Genre>());

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Genres", "At least one Genre must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenWhenMultipleSameGenresAreProvided_CreatesInstanceWithUniqueGenres()
    {
        var book = new Book(HobbitTitle, HobbitDescription, new[] { Genre.Fantasy, Genre.Fantasy });

        Assert.NotNull(book.Id);
        Assert.Equal(HobbitTitle, book.Title);
        Assert.Equal(HobbitDescription, book.Description);
        Assert.Single(HobbitGenres);
        Assert.Equivalent(book.Genres, HobbitGenres);
    }
}