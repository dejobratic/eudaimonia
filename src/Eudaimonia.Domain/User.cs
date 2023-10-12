using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class UserId : GuidId
{
    public UserId() { }

    public UserId(string value) : base(value) { }

    public UserId(Guid value) : base(value) { }
}

public abstract class User<TId> : Entity<TId>
    where TId : GuidId, new()
{
    public Text FullName { get; }
    public Text? Bio { get; }

    protected User(Text fullName, Text? bio)
        : base(new TId())
    {
        FullName = fullName;
        Bio = bio;
    }

    protected virtual void ThrowIfInvalid()
    {
        if (FullName is null) ThrowValidationException(nameof(FullName), $"{nameof(FullName)} must be specified.");
    }

    protected void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(GetType().Name, new ValidationError(propertyName, errorMessage));
}