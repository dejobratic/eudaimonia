using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddBook;

public class AddBookCommandBookFactory : IFactory<AddBookCommand, Book>
{
    public Book CreateFrom(AddBookCommand command)
    {
        return new(
            new BookId(),
            new Text(command.OriginalTitle!),
            new Language(command.OriginalLanguage!),
            new AuthorId(command.AuthorId.GetValueOrDefault()),
            CreateEdition(command.Edition)!,
            CreateGenres(command.Genres));
    }

    private static Edition? CreateEdition(EditionDto? edition)
        => edition is null
        ? null
        : new Edition(
            new EditionId(),
            new Text(edition.Title!),
            new Text(edition.Description!),
            new Language(edition.Language!),
            CreateEditionSpecs(edition.Specs)!,
            new PublisherId(edition.PublisherId.GetValueOrDefault()),
            new Year(edition.PublicationYear.GetValueOrDefault()));

    private static EditionSpecs? CreateEditionSpecs(EditionSpecsDto? specs)
        => specs is null
        ? null
        : new EditionSpecs(
            specs.PageCount.GetValueOrDefault(),
            CreateImage(specs.FrontCover)!,
            Enum.Parse<BookFormat>(specs.Format!));

    private static Image? CreateImage(ImageDto? image)
        => image is null
        ? null
        : new Image(
            new Text(image.Name),
            image.Url);

    private static IEnumerable<Genre> CreateGenres(IEnumerable<string> genres)
        => genres.Select(g => Genre.FromName<Genre>(g));
}