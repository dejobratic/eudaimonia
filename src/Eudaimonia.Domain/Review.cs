using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class ReviewId : GuidId { }

public class Review : Entity<ReviewId>
{
    public BookId BookId { get; }
    public UserId ReviewerId { get; }
    public Rating Rating { get; }
    public Comment? Comment { get; }

    public Review(
        BookId bookId,
        UserId reviewerId,
        Rating rating,
        Comment? comment) // Should Review be an aggregate root for Comment?
        : base(new ReviewId())
    {
        BookId = bookId;
        ReviewerId = reviewerId;
        Rating = rating;
        Comment = comment;

        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (BookId is null) ThrowValidationException(nameof(BookId), $"{nameof(BookId)} must be specified.");
        if (ReviewerId is null) ThrowValidationException(nameof(ReviewerId), $"{nameof(ReviewerId)} must be specified.");
        if (Rating is null) ThrowValidationException(nameof(Rating), $"{nameof(Rating)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Review), new ValidationError(propertyName, errorMessage));
}