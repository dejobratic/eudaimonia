using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Repositories;

// TODO: This needs to work with QueryDbContext and DTOs
public class BookQueryRepository :
    IGetAllBooksRepository
{
    private readonly DbContext _dbContext;

    public BookQueryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<Book>().AsNoTracking().ToListAsync(cancellationToken);
}