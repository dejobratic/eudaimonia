using Eudaimonia.Application.Books.AddAuthor;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Repositories;

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