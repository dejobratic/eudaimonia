using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Domain;

namespace Eudaimonia.Infrastructure.Persistence.Commands.Repositories;

public class BookCommandRepository :
    IAddBookRepository
{
    private readonly CommandDbContext _dbContext;

    public BookCommandRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(book, cancellationToken);
}