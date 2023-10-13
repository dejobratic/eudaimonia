using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.GetAllBooks;

public interface IGetAllBooksRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
}