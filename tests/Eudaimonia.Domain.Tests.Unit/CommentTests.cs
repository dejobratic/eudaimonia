using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class CommentTests
{
    private static readonly CommentId Id = new();
    private static readonly UserId CommenterId = new();
    private static readonly Text Text = new("Great book!");
    private static readonly DateTime CreatedAt = DateTime.UtcNow;

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var comment = new Comment(Id, CommenterId, Text, CreatedAt);

        Assert.NotNull(comment);
        Assert.Equal(Id, comment.Id);
        Assert.Equal(CommenterId, comment.CommenterId);
        Assert.Equal(Text, comment.Text);
        Assert.Equal(CreatedAt, comment.CreatedAt);
    }

    [Fact]
    public void Constructor_WhenComenterIdIsNull_ThrowsException()
    {
        static Comment action() => new(Id, null!, Text, CreatedAt);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Comment with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("CommenterId", "CommenterId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenTextIsNull_ThrowsException()
    {
        static Comment action() => new(Id, CommenterId, null!, CreatedAt);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Comment with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Text", "Text must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenCreatedAtIsDefault_ThrowsException()
    {
        static Comment action() => new(Id, CommenterId, Text, default);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Comment with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("CreatedAt", "CreatedAt must be specified.") }, exception.Errors);
    }
}