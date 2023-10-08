using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books.AddBook;

public interface IAddBookRepository
{
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
}
