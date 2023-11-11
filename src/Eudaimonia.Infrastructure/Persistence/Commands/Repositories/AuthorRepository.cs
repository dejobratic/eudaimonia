using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Exceptions;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly CommandDbContext _dbContext;

    public AuthorRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Author> GetByIdAsync(AuthorId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindAsync<Author>(keyValues: new[] { id }, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Author), id);
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(author, cancellationToken);

    public Task UpdateAsync(Author entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task DeleteAsync(Author entity, CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Remove(entity));
}