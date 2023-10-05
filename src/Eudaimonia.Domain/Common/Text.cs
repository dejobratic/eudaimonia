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

    private void ThrowIfInvalid()
    {
        if (string.IsNullOrEmpty(Value))
            throw new ValidationException(nameof(Text), new ValidationError(nameof(Value), "Value cannot be null or empty."));
    }

    public override string ToString() => Value;
}
