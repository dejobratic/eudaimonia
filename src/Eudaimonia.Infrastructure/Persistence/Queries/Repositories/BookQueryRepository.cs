using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class BookQueryRepository :
    IGetAllBooksRepository
{
    private readonly QueryDbContext _dbContext;

    public BookQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<BookDto>().ToListAsync(cancellationToken);
}