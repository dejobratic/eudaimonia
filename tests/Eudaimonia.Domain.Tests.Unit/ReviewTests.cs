using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class ReviewTests
{
    private static readonly BookId BookId = new();
    private static readonly UserId ReviewerId = new();
    private static readonly Rating Rating = Rating.FiveStar;
    private static readonly Comment? Comment = new(ReviewerId, new Text("Great book!"), DateTime.UtcNow);

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var review = new Review(BookId, ReviewerId, Rating, Comment);

        Assert.NotNull(review);
        Assert.NotNull(review.Id);
        Assert.Equal(BookId, review.BookId);
        Assert.Equal(ReviewerId, review.ReviewerId);
        Assert.Equal(Rating, review.Rating);
        Assert.Equal(Comment, review.Comment);
    }

    [Fact]
    public void Constructor_WhenBookIdIsNull_ThrowsException()
    {
        static Review action() => new(null!, ReviewerId, Rating, Comment);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("BookId", "BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenReviewerIdIsNull_ThrowsException()
    {
        static Review action() => new(BookId, null!, Rating, Comment);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("ReviewerId", "ReviewerId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenRatingIsNull_ThrowsException()
    {
        static Review action() => new(BookId, ReviewerId, null!, Comment);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Rating", "Rating must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenCommentIsNull_CreatesInstance()
    {
        var review = new Review(BookId, ReviewerId, Rating, null);

        Assert.NotNull(review);
        Assert.NotNull(review.Id);
        Assert.Equal(BookId, review.BookId);
        Assert.Equal(ReviewerId, review.ReviewerId);
        Assert.Equal(Rating, review.Rating);
        Assert.Null(review.Comment);
    }
}