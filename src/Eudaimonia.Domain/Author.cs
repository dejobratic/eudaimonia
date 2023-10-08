namespace Eudaimonia.Domain;

public sealed class Author : User
{
    private readonly HashSet<BookId> _authoredBookIds;
    public IEnumerable<BookId> AuthoredBookIds => _authoredBookIds;


    public Author(Text fullName, Text? bio, IEnumerable<BookId> bookIds)
        : base(fullName, bio)
    {
        _authoredBookIds = bookIds?.ToHashSet() ?? new HashSet<BookId>();

        ThrowIfInvalid();
    }

    protected override void ThrowIfInvalid()
    {
        base.ThrowIfInvalid();
        if (!AuthoredBookIds.Any()) ThrowValidationException(nameof(AuthoredBookIds), $"At least one {nameof(BookId)} must be specified.");
    }
}