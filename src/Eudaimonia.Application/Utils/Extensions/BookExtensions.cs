using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Extensions;

public static class BookExtensions
{
    public static BookDto ToDto(this Book book)
        => new()
        {
            Id = book.Id.Value,
            OriginalTitle = book.OriginalTitle.Value,
            OriginalLanguage = book.OriginalLanguage.Code,
            AuthorId = book.AuthorId.Value,
            DefaultEditionId = book.DefaultEditionId.Value,
            DefaultEdition = book.Editions.First(edition => edition.Id == book.DefaultEditionId).ToDto(),
            Genres = book.Genres.Select(g => g.Name).ToList(),
        };

    private static EditionDto ToDto(this Edition edition)
        => new()
        {
            Id = edition.Id.Value,
            Title = edition.Title.Value,
            Description = edition.Description.Value,
            Language = edition.Language.Code,
            Specs = edition.Specs.ToDto(),
            PublisherId = edition.PublisherId.Value,
            PublicationYear = edition.PublicationYear.Value,
        };

    private static EditionSpecsDto ToDto(this EditionSpecs specs)
        => new()
        {
            PageCount = specs.PageCount,
            FrontCover = specs.FrontCover.ToDto(),
            Format = specs.Format.ToString(),
        };

    private static ImageDto ToDto(this Image image)
        => new()
        {
            Name = image.Name.Value,
            Url = image.Url
        };

    private static ReviewSummaryDto ToDto(this ReviewSummary reviewSummary)
        => new()
        {
            ReviewCount = (int)reviewSummary.ReviewCount,
            RatingCount = (int)reviewSummary.RatingCount,
            FiveStarRatingCount = (int)reviewSummary.FiveStarRatingCount,
            FourStarRatingCount = (int)reviewSummary.FourStarRatingCount,
            ThreeStarRatingCount = (int)reviewSummary.ThreeStarRatingCount,
            TwoStarRatingCount = (int)reviewSummary.TwoStarRatingCount,
            OneStarRatingCount = (int)reviewSummary.OneStarRatingCount,
            AverageRating = reviewSummary.AverageRating,
        };
}