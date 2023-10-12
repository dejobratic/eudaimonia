using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class CommentId : GuidId
{
    public CommentId() { }

    public CommentId(string value) : base(value) { }

    public CommentId(Guid value) : base(value) { }
}

// Consider adding upvotes / downvotes for comments.
public class Comment : Entity<CommentId>
{
    public UserId CommenterId { get; }
    public Text Text { get; }
    public DateTime CreatedAt { get; }

    private Comment() { } // Required by EF core.

    public Comment(UserId commenterId, Text text, DateTime createdAt)
    {
        CommenterId = commenterId;
        Text = text;
        CreatedAt = createdAt;

        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (CommenterId is null) ThrowValidationException(nameof(CommenterId), $"{nameof(CommenterId)} must be specified.");
        if (Text is null) ThrowValidationException(nameof(Text), $"{nameof(Text)} must be specified.");
        if (CreatedAt == default) ThrowValidationException(nameof(CreatedAt), $"{nameof(CreatedAt)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Comment), new ValidationError(propertyName, errorMessage));
}