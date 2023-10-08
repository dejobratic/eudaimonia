using Eudaimonia.Application.Dtos;

namespace Eudaimonia.Application.Books.GetAllBooks;

public interface IGetAllBooksRepository
{
    Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
