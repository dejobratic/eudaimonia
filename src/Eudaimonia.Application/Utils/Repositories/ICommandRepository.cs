using Eudaimonia.Domain.Kernel;

namespace Eudaimonia.Application.Utils.Repositories;

public interface ICommandRepository<TEntity, TId>
    where TEntity : Entity<TId>
{
    Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}