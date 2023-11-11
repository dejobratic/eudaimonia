using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands.Builders;

public class BookBuilder
{
    private BookId _id = new();
    private Text _originalTitle = new("The Hobbit");
    private Language _originalLanguage = new("en");
    private AuthorId _authorId = new();
    private Edition _edition = new EditionBuilder().TheHobbit.Build();
    private IEnumerable<Genre> _genres = new[] { Genre.Fantasy, Genre.Adventure };

    public BookBuilder TheHobbit
        => WithOriginalTitle("The Hobbit")
            .WithOriginalLanguage("en")
            .WithEdition(new EditionBuilder().TheHobbit.Build())
            .WithGenres(new[] { Genre.Fantasy, Genre.Adventure });

    public BookBuilder TheLordOfTheRings
        => WithOriginalTitle("The Lord of the Rings")
            .WithOriginalLanguage("en")
            .WithEdition(new EditionBuilder().TheLordOfTheRings.Build())
            .WithGenres(new[] { Genre.Fantasy, Genre.Adventure });

    public BookBuilder WithId(BookId id)
    {
        _id = id;
        return this;
    }

    public BookBuilder WithOriginalTitle(string originalTitle)
    {
        _originalTitle = new Text(originalTitle);
        return this;
    }

    public BookBuilder WithOriginalLanguage(string originalLanguage)
    {
        _originalLanguage = new Language(originalLanguage);
        return this;
    }

    public BookBuilder WithAuthorId(AuthorId authorId)
    {
        _authorId = authorId;
        return this;
    }

    public BookBuilder WithEdition(Edition edition)
    {
        _edition = edition;
        return this;
    }

    public BookBuilder WithGenres(IEnumerable<Genre> genres)
    {
        _genres = genres;
        return this;
    }

    public Book Build()
        => new(
            _id,
            _originalTitle,
            _originalLanguage,
            _authorId,
            _edition,
            _genres);
}