using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Queries.Builders;

public class BookDtoBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _originalTitle = "The Hobbit";
    private string _originalLanguage = "en";
    private Guid _authorId = Guid.NewGuid();
    private Guid _defaultEditionId = Guid.NewGuid();
    private List<string> _genres = new() { "Fantasy", "Adventure" };

    public BookDtoBuilder TheHobbit
        => WithOriginalTitle("The Hobbit")
            .WithOriginalLanguage("en") 
            .WithGenres(new List<string> { "Fantasy", "Adventure" });

    public BookDtoBuilder TheLordOfTheRings
        => WithOriginalTitle("The Lord of the Rings")
            .WithOriginalLanguage("en")
            .WithGenres(new List<string> { "Fantasy", "Adventure" });

    public BookDtoBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public BookDtoBuilder WithOriginalTitle(string originalTitle)
    {
        _originalTitle = originalTitle;
        return this;
    }

    public BookDtoBuilder WithOriginalLanguage(string originalLanguage)
    {
        _originalLanguage = originalLanguage;
        return this;
    }

    public BookDtoBuilder WithAuthorId(Guid authorId)
    {
        _authorId = authorId;
        return this;
    }

    public BookDtoBuilder WithDefaultEditionId(Guid defaultEditionId)
    {
        _defaultEditionId = defaultEditionId;
        return this;
    }

    public BookDtoBuilder WithGenres(List<string> genres)
    {
        _genres = genres;
        return this;
    }

    public BookDto Build()
        => new()
        {
            Id = _id,
            OriginalTitle = _originalTitle,
            OriginalLanguage = _originalLanguage,
            AuthorId = _authorId,
            DefaultEditionId = _defaultEditionId,
            Genres = _genres,
        };
}