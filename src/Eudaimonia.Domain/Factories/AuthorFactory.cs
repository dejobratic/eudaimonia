using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain.Factories;

public class AuthorFactory : IAuthorFactory
{
    private readonly IIdGenerator<Guid> _idGen;

    public AuthorFactory(IIdGenerator<Guid> idGen)
    {
        _idGen = idGen;
    }

    public Author Create(string fullName, string? bio)
        => new(
            new AuthorId(_idGen.NewId()),
            new Text(fullName),
            string.IsNullOrEmpty(bio) ? null : new Text(bio));
}