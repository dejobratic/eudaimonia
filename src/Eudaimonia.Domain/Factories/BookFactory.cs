using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Factories;

public class BookFactory : IBookFactory
{
    private readonly IIdGenerator<Guid> _idGen;
    private readonly IEditionFactory _editionFactory;

    public BookFactory(IIdGenerator<Guid> idGen, IEditionFactory editionFactory)
    {
        _idGen = idGen;
        _editionFactory = editionFactory;
    }

    public Book Create(
        string originalTitle,
        string originalLanguage,
        Guid authorId,
        IEnumerable<string> genres,
        string title,
        string description,
        string language,
        int pageCount,
        string frontCoverName,
        string frontCoverUrl,
        string bookFormat,
        Guid publisherId,
        int publicationYear)
    {
        // TODO: How to validate the parameters, with the same validation rules as the Book entity?
        // Same as in FluentValidation, but without 3rd party libraries.
        var edition = _editionFactory.Create(
            title,
            description,
            language, pageCount,
            frontCoverName,
            frontCoverUrl,
            bookFormat,
            publisherId,
            publicationYear);

        return new(
            new BookId(_idGen.NewId()),
            new Text(originalTitle),
            new Language(originalLanguage),
            new AuthorId(authorId),
            edition,
            genres.Select(Genre.FromName<Genre>));
    }
}