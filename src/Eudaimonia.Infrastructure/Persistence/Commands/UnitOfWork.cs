using Eudaimonia.Application.Utils;

namespace Eudaimonia.Infrastructure.Persistence.Commands;

public class UnitOfWork : IUnitOfWork
{
    private readonly CommandDbContext _dbContext;

    public UnitOfWork(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}