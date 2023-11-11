namespace Eudaimonia.Application.Utils;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}