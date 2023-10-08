using Eudaimonia.Application.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books.AddBook;

public class AddBookCommandBookFactory : IBookFactory<AddBookCommand>
{
    public Book CreateFrom(AddBookCommand command)
    {
        return new(
            new Text(command.Title!),
            new Text(command.Description!),
            new UserId(command.AuthorId.GetValueOrDefault()),
            CreateEdition(command.Edition)!,
            CreateGenres(command.Genres));
    }

    private static Edition? CreateEdition(EditionDto? edition)
    {
        static Edition DomainEdition(EditionDto edition) => new(
            (uint)edition.PageCount,
            new Image(new Text(edition.FrontCover?.Name!), edition.FrontCover?.Url!),
            Enum.Parse<BookFormat>(edition.Format!),
            new PublisherId(edition.PublisherId!),
            new Year(edition.PublicationYear!));

        return edition is null ? null : DomainEdition(edition);
    }

    private static IEnumerable<Genre> CreateGenres(IEnumerable<string> genres)
        => genres.Select(CreateGenre);

    private static Genre CreateGenre(string genre)
    {
        _ = Enum.TryParse<Genre>(genre, out var genreEnum);
        return genreEnum;
    }
}
