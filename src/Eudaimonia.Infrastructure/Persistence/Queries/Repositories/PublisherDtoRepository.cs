using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Repositories;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class PublisherDtoRepository : IPublisherDtoRepository
{
    private readonly QueryDbContext _dbContext;

    public PublisherDtoRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PublisherDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task<IEnumerable<PublisherDto>> GetAsync(Expression<Func<PublisherDto, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<PublisherDto>());
}