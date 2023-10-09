namespace Eudaimonia.Domain;

public sealed class Author : User
{
    private readonly HashSet<BookId> _authoredBookIds;
    public IEnumerable<BookId> AuthoredBookIds => _authoredBookIds;


    public Author(Text fullName, Text? bio)
        : base(fullName, bio)
    {
        _authoredBookIds = new HashSet<BookId>();

        ThrowIfInvalid();
    }
}