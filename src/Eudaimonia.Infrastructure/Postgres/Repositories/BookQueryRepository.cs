using Eudaimonia.Application.Books.GetAllBooks;
using Eudaimonia.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Postgres.Repositories;

public class BookQueryRepository :
    IGetAllBooksRepository
{
    private readonly DbContext _dbContext;

    public BookQueryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<BookDto>().AsNoTracking().ToListAsync(cancellationToken);
}