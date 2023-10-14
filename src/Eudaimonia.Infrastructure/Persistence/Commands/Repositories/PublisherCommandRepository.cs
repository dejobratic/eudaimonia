using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class PublisherCommandRepository : IAddPublisherRepository
{
    private readonly CommandDbContext _dbContext;

    public PublisherCommandRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(publisher, cancellationToken);
}