using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Domain;

public abstract class GuidId : ValueObject<GuidId>
{
    public Guid Value { get; }

    protected GuidId()
    {
        Value = Guid.NewGuid();
    }
}