using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Factories;

public class EditionFactory : IEditionFactory
{
    private readonly IIdGenerator<Guid> _idGen;

    public EditionFactory(IIdGenerator<Guid> idGen)
    {
        _idGen = idGen;
    }

    public Edition Create(
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
        return new(
            new EditionId(_idGen.NewId()),
            new Text(title),
            new Text(description),
            new Language(language),
            new EditionSpecs(
                pageCount,
                new Image(new Text(frontCoverName), frontCoverUrl),
                Enum.Parse<BookFormat>(bookFormat)),
            new PublisherId(publisherId),
            new Year(publicationYear));
    }
}