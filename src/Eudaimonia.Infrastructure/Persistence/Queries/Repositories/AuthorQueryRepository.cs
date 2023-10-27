using Eudaimonia.Application.Features.Books.GetAuthors;
using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class AuthorQueryRepository :
    IGetAuthorsRepository
{
    private readonly QueryDbContext _dbContext;

    public AuthorQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AuthorDto>> GetAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<AuthorDto>());
}