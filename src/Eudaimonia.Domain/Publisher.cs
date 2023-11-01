using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class PublisherId : GuidId
{
    public PublisherId()
    { }

    public PublisherId(string value) : base(value)
    {
    }

    public PublisherId(Guid value) : base(value)
    {
    }
}

public sealed class Publisher : Entity<PublisherId>
{
    public Text FullName { get; } = null!;
    public Text? Bio { get; }

    private Publisher() : base()
    {
    } // Required by EF Core.

    public Publisher(
        PublisherId id,
        Text fullName,
        Text? bio)
        : base(id)
    {
        FullName = fullName;
        Bio = bio;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (FullName is null) AddError(errors, nameof(FullName), $"{nameof(FullName)} must be specified.");

        return errors;
    }
}