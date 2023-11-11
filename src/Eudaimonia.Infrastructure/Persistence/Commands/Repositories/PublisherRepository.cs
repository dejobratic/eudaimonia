using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly CommandDbContext _dbContext;

    public PublisherRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Publisher> GetByIdAsync(PublisherId id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(publisher, cancellationToken);

    public Task UpdateAsync(Publisher entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task DeleteAsync(Publisher entity, CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Remove(entity));
}