using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class BookId : GuidId
{
    public BookId()
    { }

    public BookId(string value) : base(value)
    {
    }

    public BookId(Guid value) : base(value)
    {
    }
}

public sealed class Book : Entity<BookId>
{
    public Text Title { get; }
    public Text Description { get; }
    public AuthorId AuthorId { get; }
    public Edition Edition { get; }
    public ReviewSummary ReviewSummary { get; private set; }

    private HashSet<Genre> _genres = new();

    public IEnumerable<Genre> Genres
    {
        get => _genres;
        private set { _genres = value.ToHashSet(); }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Book() : base()
    {
    } // Required by EF Core.

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Book(
        Text title,
        Text description,
        AuthorId authorId,
        Edition edition,
        IEnumerable<Genre> genres)
        : base(new BookId())
    {
        Title = title;
        Description = description;
        AuthorId = authorId;
        Edition = edition;
        ReviewSummary = new ReviewSummary();
        Genres = genres?.ToHashSet() ?? new HashSet<Genre>();

        ThrowIfInvalid();
    }

    public void AddReview(Review review)
    {
        ReviewSummary = ReviewSummary
            .AddReview(review.Rating, review.Comment);
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (Title is null) AddError(errors, nameof(Title), $"{nameof(Title)} must be specified.");
        if (Description is null) AddError(errors, nameof(Description), $"{nameof(Description)} must be specified.");
        if (AuthorId is null) AddError(errors, nameof(AuthorId), $"{nameof(AuthorId)} must be specified.");
        if (!Genres.Any()) AddError(errors, nameof(Genres), $"At least one {nameof(Genre)} must be specified.");
        if (Edition is null) AddError(errors, nameof(Edition), $"{nameof(Edition)} must be specified.");

        return errors;
    }
}