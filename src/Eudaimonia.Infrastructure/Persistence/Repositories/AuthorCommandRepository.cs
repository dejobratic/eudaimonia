using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Repositories;

public class AuthorCommandRepository : IAddAuthorRepository
{
    private readonly DbContext _dbContext;

    public AuthorCommandRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(author, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}