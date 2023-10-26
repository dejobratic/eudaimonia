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
    public Author(Text fullName, Text? bio)
        : base(fullName, bio)
    {
        ThrowIfInvalid();
    }
}