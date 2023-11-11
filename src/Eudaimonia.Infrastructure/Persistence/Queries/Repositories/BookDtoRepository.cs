using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain.Exceptions;
using System.Linq.Expressions;

namespace Eudaimonia.Infrastructure.Persistence.Queries.Repositories;

public class BookDtoRepository : IBookDtoRepository
{
    private readonly QueryDbContext _dbContext;

    public BookDtoRepository(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindAsync<BookDto>(keyValues: new object[] { id }, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException("Book", id);
    }

    public async Task<IEnumerable<BookDto>> GetAsync(Expression<Func<BookDto, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => await Task.FromResult(_dbContext.Set<BookDto>());
}