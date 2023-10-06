using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public sealed class Year : ValueObject<Year>
{
    public int Value { get; }

    public Year(int value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString("0000");
}