using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Extensions;

public static class BookExtensions
{
    public static BookDto ToDto(this Book book)
        => new()
        {
            Id = book.Id.Value,
            Title = book.Title.Value,
            Description = book.Description.Value,
            AuthorId = book.AuthorId.Value,
            Edition = book.Edition.ToDto(),
            ReviewSummary = book.ReviewSummary.ToDto(),
            Genres = book.Genres.Select(g => g.Name).ToList(),
        };

    private static EditionDto ToDto(this Edition edition)
        => new()
        {
            PageCount = (int)edition.PageCount,
            FrontCover = edition.FrontCover.ToDto(),
            Format = edition.Format.ToString(),
            PublisherId = edition.PublisherId.Value,
            PublicationYear = edition.PublicationYear.Value,
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