using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class BookId : GuidId { }

public sealed class Book : Entity<BookId>
{
    public Text Title { get; }
    public Text Description { get; }
    public UserId AuthorId { get; }
    public Edition Edition { get; }
    public ReviewSummary ReviewSummary { get; private set; }

    private readonly HashSet<Genre> _genres;
    public IEnumerable<Genre> Genres => _genres;

    public Book(
        Text title,
        Text description,
        UserId authorId,
        Edition edition,
        IEnumerable<Genre> genres)
        : base(new BookId())
    {
        Title = title;
        Description = description;
        AuthorId = authorId;
        Edition = edition;
        ReviewSummary = new ReviewSummary();
        _genres = genres?.ToHashSet() ?? new HashSet<Genre>();

        // TODO: How to validate the whole entity, with value objects and raise a single exception?
        // Same as in FluentValidation, but without 3rd party libraries.
        ThrowIfInvalid();
    }

    public void AddReview(Review review)
    {
        ReviewSummary = ReviewSummary
            .AddReview(review.Rating, review.Comment);
    }

    private void ThrowIfInvalid()
    {
        if (Title is null) ThrowValidationException(nameof(Title), $"{nameof(Title)} must be specified.");
        if (Description is null) ThrowValidationException(nameof(Description), $"{nameof(Description)} must be specified.");
        if (AuthorId is null) ThrowValidationException(nameof(AuthorId), $"{nameof(AuthorId)} must be specified.");
        if (!Genres.Any()) ThrowValidationException(nameof(Genres), $"At least one {nameof(Genre)} must be specified.");
        if (Edition is null) ThrowValidationException(nameof(Edition), $"{nameof(Edition)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Book), new ValidationError(propertyName, errorMessage));
}