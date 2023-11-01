using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class CommentId : GuidId
{
    public CommentId()
    { }

    public CommentId(string value) : base(value)
    {
    }

    public CommentId(Guid value) : base(value)
    {
    }
}

// TODO: Consider adding upvotes / downvotes for comments.
// TODO: Comment should be value object. Review can have comments and they can be ReviewComment, entity that contains comment value object???
// What about edited comments etc.??
public class Comment : Entity<CommentId>
{
    public UserId CommenterId { get; } = null!;
    public Text Text { get; } = null!;
    public DateTime CreatedAt { get; }

    private Comment() : base()
    {
    } // Required by EF Core.

    public Comment(
        CommentId id,
        UserId commenterId,
        Text text,
        DateTime createdAt)
        : base(id)
    {
        CommenterId = commenterId;
        Text = text;
        CreatedAt = createdAt;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (CommenterId is null) AddError(errors, nameof(CommenterId), $"{nameof(CommenterId)} must be specified.");
        if (Text is null) AddError(errors, nameof(Text), $"{nameof(Text)} must be specified.");
        if (CreatedAt == default) AddError(errors, nameof(CreatedAt), $"{nameof(CreatedAt)} must be specified.");

        return errors;
    }
}