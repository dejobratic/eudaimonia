using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public abstract class GuidId : ValueObject<GuidId>
{
    public Guid Value { get; private set; }

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

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (Value == Guid.Empty) AddError(errors, nameof(Value), $"{nameof(Value)} must be a valid non-empty Guid or Guid string.");

        return errors;
    }

    public override string ToString()
        => Value.ToString();

    public static implicit operator Guid(GuidId id)
        => id is null ? Guid.Empty : id.Value;
}