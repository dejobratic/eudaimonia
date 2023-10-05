namespace Eudaimonia.Domain;

public class Member : User<Member>
{
    public Member(Text fullName, Text? bio)
        : base(fullName, bio)
    {
        ThrowIfInvalid();
    }
}