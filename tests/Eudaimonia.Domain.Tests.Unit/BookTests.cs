using Eudaimonia.Domain.Validation;
using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookTests
{
    private static readonly Text HobbitTitle = new("The Hobbit");
    private static readonly Text HobbitDescription = new("The Hobbit");
    private static readonly Genre[] HobbitGenres = new[] {Genre.Fantasy }; 

    [Fact]
    public void Constructor_WhenProvidingAllRequiredParameters_CreatesInstance()
    {
        var book = new Book(HobbitTitle, HobbitDescription, HobbitGenres);

        book.Id.Should().NotBeNull();
        book.Title.Should().Be(HobbitTitle);
        book.Description.Should().Be(HobbitDescription);
        book.Genres.Should().BeEquivalentTo(HobbitGenres);
    }

    [Fact]
    public void Constructor_WhenTitleIsNull_ThrowsException()
    {
        var action = () => new Book(null!, HobbitDescription, HobbitGenres);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Book with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Title", "Title must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenDescriptionIsNull_ThrowsException()
    {
        var action = () => new Book(HobbitTitle, null!, HobbitGenres);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Book with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Description", "Description must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenGenreIsNull_ThrowsException()
    {
        var action = () => new Book(HobbitTitle, HobbitDescription, null!);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Book with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Genres", "At least one Genre must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenAtLeastOneGenreIsNotProvided_ThrowsException()
    {
        var action = () => new Book(HobbitTitle, HobbitDescription, Array.Empty<Genre>());

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Book with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Genres", "At least one Genre must be specified."),
            });
    }
}
