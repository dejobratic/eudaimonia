using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Factories;

public class PublisherFactory : IPublisherFactory
{
    private readonly IIdGenerator<Guid> _idGen;

    public PublisherFactory(IIdGenerator<Guid> idGen)
    {
        _idGen = idGen;
    }

    public Publisher Create(string fullName, string? bio)
        => new(
            new PublisherId(_idGen.NewId()),
            new Text(fullName),
            string.IsNullOrEmpty(bio) ? null : new Text(bio));
}