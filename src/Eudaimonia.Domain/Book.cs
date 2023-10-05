using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class Book : Entity<BookId>
{
    public Text Title { get; }
    public Text Description { get; }

    private readonly HashSet<Genre> _genres;
    public IReadOnlySet<Genre> Genres => _genres;

    public Book(Text title, Text description, IEnumerable<Genre> genres)
        : base(new BookId())
    {
        Title = title;
        Description = description;
        _genres = genres?.ToHashSet() ?? new HashSet<Genre>();

        // How to validate the whole entity, with value objects and raise a single exception? Same
        // as in FluentValidation, but without 3rd party libraries.
        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (Title is null) ThrowValidationException(nameof(Title), $"{nameof(Title)} must be specified.");
        if (Description is null) ThrowValidationException(nameof(Description), $"{nameof(Description)} must be specified.");
        if (!Genres.Any()) ThrowValidationException(nameof(Genres), $"At least one {nameof(Genre)} must be specified.");
    }

    private void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(nameof(Book), new ValidationError(propertyName, errorMessage));
}