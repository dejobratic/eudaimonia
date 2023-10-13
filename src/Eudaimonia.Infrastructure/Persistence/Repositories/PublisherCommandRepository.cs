using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Repositories;

public class PublisherCommandRepository : IAddPublisherRepository
{
    private readonly DbContext _dbContext;

    public PublisherCommandRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(publisher, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}