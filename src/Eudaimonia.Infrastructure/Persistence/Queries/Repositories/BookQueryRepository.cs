using Eudaimonia.Application.Features.Books.GetBookById;
using Eudaimonia.Application.Features.Books.GetBooks;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Domain.Exceptions;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class BookQueryRepository :
    IGetBookByIdRepository,
    IGetBooksRepository
{
    private readonly QueryDbContext _dbContext;

    public BookQueryRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookDto> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindAsync<BookDto>(keyValues: new object[] { id }, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException("Book", id);
    }

    public async Task<IEnumerable<BookDto>> GetAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<BookDto>());
}