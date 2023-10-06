using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

// Consider adding upvotes / downvotes for comments.
public class Comment : ValueObject<Comment>
{
    public UserId CommenterId { get; }
    public Text Text { get; }
    public DateTime CreatedAt { get; }

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