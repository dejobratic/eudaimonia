using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public class User : Entity<UserId>
{
    public Text FullName { get; }
    public Text? Bio { get; }

    public User(Text fullName, Text? bio)
        : base(new UserId())
    {
        FullName = fullName;
        Bio = bio;
    }
}