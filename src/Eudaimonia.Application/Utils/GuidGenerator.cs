using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Application.Utils;

public class GuidGenerator : IIdGenerator<Guid>
{
    public Guid NewId() => Guid.NewGuid();
}