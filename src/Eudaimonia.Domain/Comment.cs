﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class CommentId : GuidId
{
    public CommentId() { }

    public CommentId(string value) : base(value) { }

    public CommentId(Guid value) : base(value) { }
}

//TODO: Consider adding upvotes / downvotes for comments.
public class Comment : Entity<CommentId>
{
    public UserId CommenterId { get; }
    public Text Text { get; }
    public DateTime CreatedAt { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Comment() : base() { } // Required by EF Core.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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