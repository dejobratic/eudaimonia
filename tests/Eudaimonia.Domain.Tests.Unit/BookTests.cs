using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookTests
{
    private static class BookDefaults
    {
        public static readonly Text Title = new("The Hobbit");
        public static readonly Text Description = new("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937.");
        public static readonly AuthorId AuthorId = new();
        public static readonly IEnumerable<Genre> Genres = new[] { Genre.Fantasy };
        public static readonly Edition Edition = new(310, new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), BookFormat.Hardcover, new PublisherId(), new Year(1937));
        public static readonly ReviewSummary ReviewSummary = new();
    }

    private class BookBuilder
    {
        private Text _title = BookDefaults.Title;
        private Text _description = BookDefaults.Description;
        private AuthorId _authorId = BookDefaults.AuthorId;
        private IEnumerable<Genre> _genres = BookDefaults.Genres;
        private Edition _edition = BookDefaults.Edition;

        public Book Build()
            => new(
                _title,
                _description,
                _authorId,
                _edition,
                _genres);

        public BookBuilder WithTitle(Text? title)
        {
            _title = title!;
            return this;
        }

        public BookBuilder WithDescription(Text? description)
        {
            _description = description!;
            return this;
        }

        public BookBuilder WithAuthorId(AuthorId? authorId)
        {
            _authorId = authorId!;
            return this;
        }

        public BookBuilder WithEdition(Edition? edition)
        {
            _edition = edition!;
            return this;
        }

        public BookBuilder WithGenres(IEnumerable<Genre>? genres)
        {
            _genres = genres!;
            return this;
        }
    }

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var book = new BookBuilder().Build();

        Assert.NotNull(book.Id);
        Assert.Equal(BookDefaults.Title, book.Title);
        Assert.Equal(BookDefaults.Description, book.Description);
        Assert.Equal(BookDefaults.AuthorId, book.AuthorId);
        Assert.Equivalent(BookDefaults.Genres, book.Genres);
        Assert.Equivalent(BookDefaults.Edition, book.Edition);
        Assert.Equivalent(BookDefaults.ReviewSummary, book.ReviewSummary);
    }

    [Fact]
    public void Constructor_WhenTitleIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithTitle(title: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Title", "Title must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenDescriptionIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithDescription(description: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Description", "Description must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAuthorIdIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithAuthorId(authorId: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("AuthorId", "AuthorId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenGenreIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithGenres(genres: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Genres", "At least one Genre must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAtLeastOneGenreIsNotProvided_ThrowsException()
    {
        static Book action() => new BookBuilder().WithGenres(genres: Array.Empty<Genre>()).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Genres", "At least one Genre must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenWhenMultipleSameGenresAreProvided_CreatesInstanceWithUniqueGenres()
    {
        var book = new BookBuilder().WithGenres(genres: new[] { Genre.Fantasy, Genre.Fantasy }).Build();

        Assert.NotNull(book.Id);
        Assert.Equal(BookDefaults.Title, book.Title);
        Assert.Equal(BookDefaults.Description, book.Description);
        Assert.Equal(BookDefaults.AuthorId, book.AuthorId);
        Assert.Single(book.Genres);
        Assert.Equivalent(BookDefaults.Genres, book.Genres);
        Assert.Equivalent(BookDefaults.Edition, book.Edition);
        Assert.Equivalent(BookDefaults.ReviewSummary, book.ReviewSummary);
    }

    [Fact]
    public void Constructor_WhenEditionIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithEdition(edition: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Edition", "Edition must be specified.") }, exception.Errors);
    }
}