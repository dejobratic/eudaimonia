using Eudaimonia.Application.Features.Books.GetPublishers;
using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class PublisherQueryRepository :
    IGetPublishersRepository
{
    private readonly QueryDbContext _dbContext;

    public PublisherQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PublisherDto>> GetAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<PublisherDto>());
}