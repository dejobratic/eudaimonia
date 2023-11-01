namespace Eudaimonia.Domain;

public sealed class AuthorId : UserId
{
    public AuthorId()
    { }

    public AuthorId(string value) : base(value)
    {
    }

    public AuthorId(Guid value) : base(value)
    {
    }
}

public sealed class Author : User<AuthorId>
{
    public Author(AuthorId id, Text fullName, Text? bio)
        : base(id, fullName, bio)
    {
        ThrowIfInvalid();
    }
}