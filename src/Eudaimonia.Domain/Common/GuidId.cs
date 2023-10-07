using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public abstract class GuidId : ValueObject<GuidId>
{
    public Guid Value { get; }

    protected GuidId() : this(Guid.NewGuid())
    {
    }

    protected GuidId(Guid value)
    {
        Value = value;
        ThrowIfInvalid();
    }

    protected GuidId(string value)
    {
        _ = Guid.TryParse(value, out var guid);
        Value = guid;
        ThrowIfInvalid();
    }

    public void ThrowIfInvalid()
    {
        if (Value == Guid.Empty)
            throw new ValidationException(nameof(GuidId), new ValidationError(nameof(Value), "Value must be a valid non-empty Guid or Guid string."));
    }
}