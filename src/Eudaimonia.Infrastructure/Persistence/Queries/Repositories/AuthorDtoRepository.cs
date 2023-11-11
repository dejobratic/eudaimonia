using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Repositories;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class AuthorDtoRepository : IAuthorDtoRepository
{
    private readonly QueryDbContext _dbContext;

    public AuthorDtoRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<AuthorDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task<IEnumerable<AuthorDto>> GetAsync(Expression<Func<AuthorDto, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<AuthorDto>());
}