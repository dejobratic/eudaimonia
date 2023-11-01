namespace Eudaimonia.Domain;

public class Member : User<UserId>
{
    public Member(UserId id, Text fullName, Text? bio)
        : base(id, fullName, bio)
    {
        ThrowIfInvalid();
    }
}