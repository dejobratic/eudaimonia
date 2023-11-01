using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class ReviewSummaryTests
{
    private static class ReviewSummaryDefaults
    {
        public static readonly uint ReviewCount = 66_616;
        public static readonly uint RatingCount = 3_771_251;
        public static readonly uint FiveStarRatingCount = 2_017_972;
        public static readonly uint FourStarRatingCount = 1_092_338;
        public static readonly uint ThreeStarRatingCount = 456_857;
        public static readonly uint TwoStarRatingCount = 123_410;
        public static readonly uint OneStarRatingCount = 80_674;
    }

    private class ReviewSummaryBuilder
    {
        private readonly uint _reviewCount = ReviewSummaryDefaults.ReviewCount;
        private readonly uint _ratingCount = ReviewSummaryDefaults.RatingCount;
        private readonly uint _fiveStarRatingCount = ReviewSummaryDefaults.FiveStarRatingCount;
        private readonly uint _fourStarRatingCount = ReviewSummaryDefaults.FourStarRatingCount;
        private readonly uint _threeStarRatingCount = ReviewSummaryDefaults.ThreeStarRatingCount;
        private readonly uint _twoStarRatingCount = ReviewSummaryDefaults.TwoStarRatingCount;
        private readonly uint _oneStarRatingCount = ReviewSummaryDefaults.OneStarRatingCount;

        public ReviewSummary Build()
            => new(
            _reviewCount,
            _ratingCount,
            _fiveStarRatingCount,
            _fourStarRatingCount,
            _threeStarRatingCount,
            _twoStarRatingCount,
            _oneStarRatingCount);
    }

    [Fact]
    public void Constructor_Default_CreatesInstance()
    {
        var reviewSummary = new ReviewSummary();

        Assert.Equal(0u, reviewSummary.ReviewCount);
        Assert.Equal(0u, reviewSummary.RatingCount);
        Assert.Equal(0u, reviewSummary.FiveStarRatingCount);
        Assert.Equal(0u, reviewSummary.FourStarRatingCount);
        Assert.Equal(0u, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(0u, reviewSummary.TwoStarRatingCount);
        Assert.Equal(0u, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void Constructor_Default_CalculatesAverageRating()
    {
        var reviewSummary = new ReviewSummary();

        Assert.Equal(0, reviewSummary.AverageRating);
    }

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Theory]
    [InlineData(10, 10, 2, 2, 2, 2, 1)]
    [InlineData(10, 10, 2, 2, 2, 2, 3)]
    public void Constructor_WhenWhenSumOfRatingsIsNotEqualToRatingCount_ThrowsException(
        uint reviewCount,
        uint ratingCount,
        uint fiveStarRatingCount,
        uint fourStarRatingCount,
        uint threeStarRatingCount,
        uint twoStarRatingCount,
        uint oneStarRatingCount)
    {
        ReviewSummary action() => new(
            reviewCount,
            ratingCount,
            fiveStarRatingCount,
            fourStarRatingCount,
            threeStarRatingCount,
            twoStarRatingCount,
            oneStarRatingCount);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for ReviewSummary with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("RatingCount", "RatingCount must be the same as the sum of star ratings.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CalculatesAverageRating()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();

        Assert.Equal(4.28, reviewSummary.AverageRating);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithComment_IncreasesRatingsAndReviews()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.FiveStar, new Comment(new CommentId(), new UserId(), new Text("Great book!"), DateTime.UtcNow));

        Assert.Equal(ReviewSummaryDefaults.ReviewCount + 1, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount + 1, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithoutComment_IncreasesRatingsOnly()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.FiveStar, null);

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount + 1, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithFourStarRating_IncreasesFourStarRatingCount()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.FourStar, null);

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount + 1, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithThreeStarRating_IncreasesThreeStarRatingCount()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.ThreeStar, null);

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount + 1, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithTwoStarRating_IncreasesTwoStarRatingCount()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.TwoStar, null);

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount + 1, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount, reviewSummary.OneStarRatingCount);
    }

    [Fact]
    public void AddReview_WhenAddingReviewWithOneStarRating_IncreasesOneStarRatingCount()
    {
        var reviewSummary = new ReviewSummaryBuilder().Build();
        reviewSummary = reviewSummary.AddReview(Rating.OneStar, null);

        Assert.Equal(ReviewSummaryDefaults.ReviewCount, reviewSummary.ReviewCount);
        Assert.Equal(ReviewSummaryDefaults.RatingCount + 1, reviewSummary.RatingCount);
        Assert.Equal(ReviewSummaryDefaults.FiveStarRatingCount, reviewSummary.FiveStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.FourStarRatingCount, reviewSummary.FourStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.ThreeStarRatingCount, reviewSummary.ThreeStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.TwoStarRatingCount, reviewSummary.TwoStarRatingCount);
        Assert.Equal(ReviewSummaryDefaults.OneStarRatingCount + 1, reviewSummary.OneStarRatingCount);
    }
}