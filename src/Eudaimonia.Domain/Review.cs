using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class ReviewId : GuidId
{
    public ReviewId()
    { }

    public ReviewId(string value) : base(value)
    {
    }

    public ReviewId(Guid value) : base(value)
    {
    }
}

public class Review : Entity<ReviewId>
{
    public BookId BookId { get; }
    public UserId ReviewerId { get; }
    public Rating Rating { get; }
    public Comment? Comment { get; }
    public DateTime CreatedAt { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Review() : base()
    {
    } // Required by EF Core.

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (BookId is null) AddError(errors, nameof(BookId), $"{nameof(BookId)} must be specified.");
        if (ReviewerId is null) AddError(errors, nameof(ReviewerId), $"{nameof(ReviewerId)} must be specified.");
        if (Rating is null) AddError(errors, nameof(Rating), $"{nameof(Rating)} must be specified.");
        if (CreatedAt == default) AddError(errors, nameof(CreatedAt), $"{nameof(CreatedAt)} must be specified.");

        return errors;
    }
}