using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Publisher : Entity<PublisherId>
{
    public Text FullName { get; }
    public Text? Bio { get; }
    
    private readonly List<BookId> _publishedBookIds;
    public IReadOnlyList<BookId> PublishedBookIds => _publishedBookIds.AsReadOnly();

    public Publisher(Text fullName, Text? bio, IEnumerable<BookId> publishedBookIds)
        : base(new PublisherId())
    {
        FullName = fullName;
        Bio = bio;
        _publishedBookIds = publishedBookIds.ToList();
    }
}