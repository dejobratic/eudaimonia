using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class AuthorCommandRepository : IAddAuthorRepository
{
    private readonly CommandDbContext _dbContext;

    public AuthorCommandRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(author, cancellationToken);
}