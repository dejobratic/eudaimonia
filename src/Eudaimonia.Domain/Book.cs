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
    public Text OriginalTitle { get; } = null!;
    public Language OriginalLanguage { get; } = null!;
    public AuthorId AuthorId { get; } = null!;

    public EditionId DefaultEditionId { get; } = null!;

    private HashSet<Edition> _editions = new();
    public IEnumerable<Edition> Editions
    {
        get => _editions;
        private set { _editions = value.ToHashSet(); }
    }

    private HashSet<Genre> _genres = new();
    public IEnumerable<Genre> Genres
    {
        get => _genres;
        private set { _genres = value.ToHashSet(); }
    }

    private Book() : base()
    {
    } // Required by EF Core.

    public Book(
        BookId id,
        Text originalTitle,
        Language originalLanguage,
        AuthorId authorId,
        Edition edition,
        IEnumerable<Genre> genres)
        : base(id)
    {
        OriginalTitle = originalTitle;
        OriginalLanguage = originalLanguage;
        AuthorId = authorId;
        if (edition is not null)
        {
            _editions.Add(edition);
            DefaultEditionId = edition.Id;
        }
        _genres = genres?.ToHashSet() ?? new HashSet<Genre>();

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (OriginalTitle is null) AddError(errors, nameof(OriginalTitle), $"{nameof(OriginalTitle)} must be specified.");
        if (OriginalLanguage is null) AddError(errors, nameof(OriginalLanguage), $"{nameof(OriginalLanguage)} must be specified.");
        if (AuthorId is null) AddError(errors, nameof(AuthorId), $"{nameof(AuthorId)} must be specified.");
        if (!Editions.Any()) AddError(errors, nameof(Editions), $"At least one {nameof(Edition)} must be specified.");
        if (!Genres.Any()) AddError(errors, nameof(Genres), $"At least one {nameof(Genre)} must be specified.");

        return errors;
    }
}