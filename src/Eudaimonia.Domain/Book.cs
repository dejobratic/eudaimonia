using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Book : Entity<BookId>
{
    public Text Title { get; }
    public Text Description { get; }

    private readonly List<Genre> _genres;
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    public Book(Text title, Text description, IEnumerable<Genre> genres)
        : base(new BookId())
    {
        Title = title;
        Description = description;
        _genres = genres.ToList();

        // How to validate the whole entity, with value objects and raise a single exception?
        // Same as in FluentValidation, but without 3rd party libraries.
    }
}
