using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class BookTests
{
    private static class BookDefaults
    {
        public static readonly BookId Id = new();
        public static readonly Text OriginalTitle = new("The Hobbit");
        public static readonly Language OriginalLanguage = new("en"); 
        public static readonly AuthorId AuthorId = new();
        public static readonly Edition Edition = new(
            new EditionId(),
            new Text("The Hobbit"),
            new Text("Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937."),
            new Language("en"),
            new EditionSpecs(310, new Image(new Text("Cover.jpg"), "https://pictures.abebooks.com/inventory/31499487055.jpg"), BookFormat.Hardcover),
            new PublisherId(),
            new Year(1937));
        public static readonly IEnumerable<Genre> Genres = new[] { Genre.Fantasy };
    }

    private class BookBuilder
    {
        private Text _originalTitle = BookDefaults.OriginalTitle;
        private Language _originalLanguage = BookDefaults.OriginalLanguage;
        private AuthorId _authorId = BookDefaults.AuthorId;
        private Edition _edition = BookDefaults.Edition;
        private IEnumerable<Genre> _genres = BookDefaults.Genres;

        public Book Build()
            => new(
                BookDefaults.Id,
                _originalTitle,
                _originalLanguage,
                _authorId,
                _edition,
                _genres);

        public BookBuilder WithOriginalTitle(Text? originalTitle)
        {
            _originalTitle = originalTitle!;
            return this;
        }

        public BookBuilder WithOriginalLanguage(Language? originalLanguage)
        {
            _originalLanguage = originalLanguage!;
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

        Assert.NotNull(book);
        Assert.Equal(BookDefaults.Id, book.Id);
        Assert.Equal(BookDefaults.OriginalTitle, book.OriginalTitle);
        Assert.Equal(BookDefaults.OriginalLanguage, book.OriginalLanguage);
        Assert.Equal(BookDefaults.AuthorId, book.AuthorId);
        Assert.Equal(BookDefaults.Edition.Id, book.DefaultEditionId);
        Assert.Single(book.Editions);
        Assert.Equivalent(BookDefaults.Edition, book.Editions.First());
        Assert.Equivalent(BookDefaults.Genres, book.Genres);
    }

    [Fact]
    public void Constructor_WhenOriginalTitleIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithOriginalTitle(originalTitle: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("OriginalTitle", "OriginalTitle must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenOriginalLanguageIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithOriginalLanguage(originalLanguage: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("OriginalLanguage", "OriginalLanguage must be specified.") }, exception.Errors);
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
    public void Constructor_WhenEditionIsNull_ThrowsException()
    {
        static Book action() => new BookBuilder().WithEdition(edition: null).Build();

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Book with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Editions", "At least one Edition must be specified.") }, exception.Errors);
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

        Assert.NotNull(book);
        Assert.Equal(BookDefaults.Id, book.Id);
        Assert.Equal(BookDefaults.OriginalTitle, book.OriginalTitle);
        Assert.Equal(BookDefaults.OriginalLanguage, book.OriginalLanguage);
        Assert.Equal(BookDefaults.AuthorId, book.AuthorId);
        Assert.Equal(BookDefaults.Edition.Id, book.DefaultEditionId);
        Assert.Single(book.Editions);
        Assert.Equivalent(BookDefaults.Edition, book.Editions.First());
        Assert.Single(book.Genres);
        Assert.Equivalent(BookDefaults.Genres, book.Genres);
    }
}