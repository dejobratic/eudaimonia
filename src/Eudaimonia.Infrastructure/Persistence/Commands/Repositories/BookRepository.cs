using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class BookRepository : IBookRepository
{
    private readonly CommandDbContext _dbContext;

    public BookRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Book> GetByIdAsync(BookId id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(book, cancellationToken);

    public Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task DeleteAsync(Book entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}