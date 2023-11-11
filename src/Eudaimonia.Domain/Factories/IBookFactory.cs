namespace Eudaimonia.Domain.Factories;

public interface IBookFactory : IEntityFactory
{
    Book Create(
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
        int publicationYear);
}