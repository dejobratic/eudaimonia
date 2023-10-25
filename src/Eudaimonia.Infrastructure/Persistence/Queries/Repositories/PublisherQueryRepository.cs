using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class PublisherQueryRepository :
    IGetAllPublishersRepository
{
    private readonly QueryDbContext _dbContext;

    public PublisherQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PublisherDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<PublisherDto>().ToListAsync(cancellationToken);
}