using System.Linq.Expressions;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IQueryRepository<TDto, TId>
    where TDto : class, new()
{
    Task<TDto> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TDto>> GetAsync(Expression<Func<TDto, bool>>? predicate = null, CancellationToken cancellationToken = default);
}