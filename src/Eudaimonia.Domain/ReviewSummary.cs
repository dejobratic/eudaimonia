using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class ReviewSummary : ValueObject<ReviewSummary>
{
    public uint ReviewCount { get; }
    public uint RatingCount { get; }
    public uint FiveStarRatingCount { get; }
    public uint FourStarRatingCount { get; }
    public uint ThreeStarRatingCount { get; }
    public uint TwoStarRatingCount { get; }
    public uint OneStarRatingCount { get; }

    public double AverageRating { get; }

    public ReviewSummary()
        : this(0, 0, 0, 0, 0, 0, 0)
    {
    }

    public ReviewSummary(
        uint reviewCount,
        uint ratingCount,
        uint fiveStarRatingCount,
        uint fourStarRatingCount,
        uint threeStarRatingCount,
        uint twoStarRatingCount,
        uint oneStarRatingCount)
    {
        ReviewCount = reviewCount;
        RatingCount = ratingCount;
        FiveStarRatingCount = fiveStarRatingCount;
        FourStarRatingCount = fourStarRatingCount;
        ThreeStarRatingCount = threeStarRatingCount;
        TwoStarRatingCount = twoStarRatingCount;
        OneStarRatingCount = oneStarRatingCount;

        ThrowIfInvalid();
        AverageRating = CalculateAverageRating();
    }

    private double CalculateAverageRating()
    {
        return RatingCount == 0 ? 0 : Math.Round(
            (5 * FiveStarRatingCount +
            4 * FourStarRatingCount +
            3 * ThreeStarRatingCount +
            2 * TwoStarRatingCount +
            OneStarRatingCount) / (double)RatingCount, 2);
    }

    public ReviewSummary AddReview(Rating rating, Comment? comment)
    {
        return new(
            IncrementIfTrue(ReviewCount, comment is not null),
            IncrementIfTrue(RatingCount, true),
            IncrementIfTrue(FiveStarRatingCount, rating == Rating.FiveStar),
            IncrementIfTrue(FourStarRatingCount, rating == Rating.FourStar),
            IncrementIfTrue(ThreeStarRatingCount, rating == Rating.ThreeStar),
            IncrementIfTrue(TwoStarRatingCount, rating == Rating.TwoStar),
            IncrementIfTrue(OneStarRatingCount, rating == Rating.OneStar));
    }

    private static uint IncrementIfTrue(uint count, bool condition)
        => count + (condition ? 1u : 0u);

    private void ThrowIfInvalid()
    {
        if (RatingCount != FiveStarRatingCount + FourStarRatingCount + ThreeStarRatingCount + TwoStarRatingCount + OneStarRatingCount)
            ThrowValidationException(nameof(RatingCount), $"{nameof(RatingCount)} must be the same as the sum of star ratings.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(ReviewSummary), new ValidationError(propertyName, errorMessage));
}