namespace Eudaimonia.Domain;

public class Member : User
{
    public Member(Text fullName, Text? bio)
        : base(fullName, bio)
    {
        ThrowIfInvalid();
    }
}