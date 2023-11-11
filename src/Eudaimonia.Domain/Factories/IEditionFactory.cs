namespace Eudaimonia.Domain.Factories;

public interface IEditionFactory : IEntityFactory
{
    Edition Create(
        string title,
        string description,
        string language,
        int pageCount,
        string frontCoverName,
        string frontCoverUrl,
        string bookFormat,
        Guid publisherId,
        int publicationYear);
}