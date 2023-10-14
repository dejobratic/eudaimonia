using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class AuthorQueryRepository :
    IGetAllAuthorsRepository
{
    private readonly QueryDbContext _dbContext;

    public AuthorQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<AuthorDto>().ToListAsync(cancellationToken);
}