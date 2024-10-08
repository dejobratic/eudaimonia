﻿using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain.Tests.Unit;

public class ReviewTests
{
    private static readonly ReviewId Id = new();
    private static readonly BookId BookId = new();
    private static readonly UserId ReviewerId = new();
    private static readonly Rating Rating = Rating.FiveStar;
    private static readonly Text? Comment = new("Great book!");
    private static readonly DateTime CreatedAt = DateTime.UtcNow;

    [Fact]
    public void Constructor_WhenAllRequiredParametersAreProvided_CreatesInstance()
    {
        var review = new Review(Id, BookId, ReviewerId, Rating, Comment, CreatedAt);

        Assert.NotNull(review);
        Assert.Equal(Id, review.Id);
        Assert.Equal(ReviewerId, review.ReviewerId);
        Assert.Equal(Rating, review.Rating);
        Assert.Equal(Comment, review.Comment);
        Assert.Equal(CreatedAt, review.CreatedAt);
    }

    [Fact]
    public void Constructor_WhenBookIdIsNull_ThrowsException()
    {
        static Review action() => new(Id, null!, ReviewerId, Rating, Comment, CreatedAt);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("BookId", "BookId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenReviewerIdIsNull_ThrowsException()
    {
        static Review action() => new(Id, BookId, null!, Rating, Comment, CreatedAt);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("ReviewerId", "ReviewerId must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenRatingIsNull_ThrowsException()
    {
        static Review action() => new(Id, BookId, ReviewerId, null!, Comment, CreatedAt);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("Rating", "Rating must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenCreatedAtIsDefault_ThrowsException()
    {
        static Review action() => new(Id, BookId, ReviewerId, Rating, Comment, default);

        var exception = Assert.Throws<ValidationException>(action);
        Assert.Equal("Validation failed for Review with 1 error(s).", exception.Message);
        Assert.Equivalent(new[] { new ValidationError("CreatedAt", "CreatedAt must be specified.") }, exception.Errors);
    }

    [Fact]
    public void Constructor_WhenCommentIsNull_CreatesInstance()
    {
        var review = new Review(Id, BookId, ReviewerId, Rating, null, CreatedAt);

        Assert.NotNull(review);
        Assert.Equal(Id, review.Id);
        Assert.Equal(ReviewerId, review.ReviewerId);
        Assert.Equal(Rating, review.Rating);
        Assert.Null(review.Comment);
        Assert.Equal(CreatedAt, review.CreatedAt);
    }
}