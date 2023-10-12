using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class PublisherId : GuidId
{
    public PublisherId() { }

    public PublisherId(string value) : base(value) { }

    public PublisherId(Guid value) : base(value) { }
}

public sealed class Publisher : Entity<PublisherId>
{
    public Text FullName { get; }
    public Text? Bio { get; }

    private Publisher() : base() { } // Required by EF Core.

    public Publisher(Text fullName, Text? bio)
        : base(new PublisherId())
    {
        FullName = fullName;
        Bio = bio;

        ThrowIfInvalid();
    }

    private void ThrowIfInvalid()
    {
        if (FullName is null) ThrowValidationException(nameof(FullName), $"{nameof(FullName)} must be specified.");
    }

    private static void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(typeof(Publisher).Name, new ValidationError(propertyName, errorMessage));
}