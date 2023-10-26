using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class UserId : GuidId
{
    public UserId()
    { }

    public UserId(string value) : base(value)
    {
    }

    public UserId(Guid value) : base(value)
    {
    }
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

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (FullName is null) AddError(errors, nameof(FullName), "FullName must be specified.");

        return errors;
    }
}