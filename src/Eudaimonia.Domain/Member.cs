namespace Eudaimonia.Domain;

public class Member : User<UserId>
{
    public Member(Text fullName, Text? bio)
        : base(fullName, bio)
    {
        ThrowIfInvalid();
    }
}