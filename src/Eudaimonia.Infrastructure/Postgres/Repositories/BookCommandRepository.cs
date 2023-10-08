using Eudaimonia.Application.Books.AddBook;
using Eudaimonia.Domain;
using Eudaimonia.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Postgres.Repositories;

public class BookCommandRepository :
    IAddBookRepository
{
    private readonly DbContext _dbContext;

    public BookCommandRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(book.ToDto(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}