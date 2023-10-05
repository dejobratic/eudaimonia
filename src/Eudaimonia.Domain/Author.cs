namespace Eudaimonia.Domain;

public sealed class Author : User
{
    private readonly List<BookId> _bookIds;
    public IReadOnlyList<BookId> BookIds => _bookIds.AsReadOnly();

    public Author(Text fullName, Text? bio, IEnumerable<BookId> bookIds)
        : base(fullName, bio)
    {
        _bookIds = bookIds.ToList();
    }
}
