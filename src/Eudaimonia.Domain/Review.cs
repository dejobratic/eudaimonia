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
    public BookId BookId { get; } = null!;
    public UserId ReviewerId { get; } = null!;
    public Rating Rating { get; } = null!;
    public Text? Comment { get; }
    public DateTime CreatedAt { get; }

    private Review() : base()
    {
    } // Required by EF Core.

    public Review(
        ReviewId id,
        BookId bookId,
        UserId reviewerId,
        Rating rating,
        Text? comment,
        DateTime createdAt)
        : base(id)
    {
        BookId = bookId;
        ReviewerId = reviewerId;
        Rating = rating;
        Comment = comment;
        CreatedAt = createdAt;

        ThrowIfInvalid();
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