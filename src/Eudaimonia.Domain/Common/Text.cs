using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public sealed class Text : ValueObject<Text>
{
    public string Value { get; }

    public Text(string value)
    {
        Value = value;
        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (string.IsNullOrEmpty(Value)) AddError(errors, nameof(Value), $"{nameof(Value)} cannot be null or empty.");

        return errors;
    }

    public override string ToString() => Value;
}