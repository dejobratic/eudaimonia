using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class ReviewId : GuidId
{
    public ReviewId() { }

    public ReviewId(string value) : base(value) { }

    public ReviewId(Guid value) : base(value) { }
}

public class Review : Entity<ReviewId>
{
    public BookId BookId { get; }
    public UserId ReviewerId { get; }
    public Rating Rating { get; }
    public Comment? Comment { get; }
    public DateTime CreatedAt { get; }

    private Review() : base() { } // Required by EF Core.

    public Review(
        BookId bookId,
        UserId reviewerId,
        Rating rating,
        Text? comment,
        DateTime createdAt)
        : base(new ReviewId())
    {
        BookId = bookId;
        ReviewerId = reviewerId;
        Rating = rating;
        CreatedAt = createdAt;

        ThrowIfInvalid();

        if (comment is not null)
            Comment = new Comment(reviewerId, comment, createdAt);
    }

    private void ThrowIfInvalid()
    {
        if (BookId is null) ThrowValidationException(nameof(BookId), $"{nameof(BookId)} must be specified.");
        if (ReviewerId is null) ThrowValidationException(nameof(ReviewerId), $"{nameof(ReviewerId)} must be specified.");
        if (Rating is null) ThrowValidationException(nameof(Rating), $"{nameof(Rating)} must be specified.");
        if (CreatedAt == default) ThrowValidationException(nameof(CreatedAt), $"{nameof(CreatedAt)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Review), new ValidationError(propertyName, errorMessage));
}