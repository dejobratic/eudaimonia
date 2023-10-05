using Eudaimonia.Domain.Validation;
using FluentAssertions;

namespace Eudaimonia.Domain.Tests.Unit;

public class AuthorTests
{
    private static readonly Text AuthorFullName = new("J.R.R. Tolkien");
    private static readonly Text AuthorBio = new("John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic.");
    private static readonly BookId[] AuthorBookIds = new[] {new BookId(), new BookId() }; 

    [Fact]
    public void Constructor_WhenProvidingAllRequiredParameters_CreatesInstance()
    {
        var user = new Author(AuthorFullName, AuthorBio, AuthorBookIds);

        user.Id.Should().NotBeNull();
        user.FullName.Should().Be(AuthorBookIds);
        user.Bio.Should().Be(AuthorBio);
    }

    [Fact]
    public void Constructor_WhenFullNameIsNull_ThrowsException()
    {
        var action = () => new Author(null!, AuthorBio, AuthorBookIds);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Author with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("FullName", "FullName must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenBioIsNull_CreatesInstance()
    {
        var user = new Author(AuthorFullName, null, AuthorBookIds);

        user.Id.Should().NotBeNull();
        user.FullName.Should().Be(AuthorBookIds);
        user.Bio.Should().BeNull();
    }

    [Fact]
    public void Constructor_WhenAuthoredBooksIsNull_ThrowsException()
    {
        var action = () => new Author(AuthorFullName, AuthorBio, null!);

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Author with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Genres", "At least one BookId must be specified."),
            });
    }

    [Fact]
    public void Constructor_WhenAtLeastOneAuthoredBookIdIsNotProvided_ThrowsException()
    {
        var action = () => new Author(AuthorFullName, AuthorBio, Array.Empty<BookId>());

        action.Should().Throw<ValidationException>()
            .WithMessage("Validation failed for Author with 1 error(s).")
            .And.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationError("Genres", "At least one BookId must be specified."),
            });
    }
}