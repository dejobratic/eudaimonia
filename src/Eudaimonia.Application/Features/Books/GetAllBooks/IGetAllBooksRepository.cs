using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetAllBooks;

public interface IGetAllBooksRepository
{
    Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
}