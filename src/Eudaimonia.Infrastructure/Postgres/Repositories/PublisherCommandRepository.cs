using Eudaimonia.Application.Books.AddPublisher;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Postgres.Repositories;

public class PublisherCommandRepository : IAddPublisherRepository
{
    private readonly DbContext _dbContext;

    public PublisherCommandRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(publisher.ToDto(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}