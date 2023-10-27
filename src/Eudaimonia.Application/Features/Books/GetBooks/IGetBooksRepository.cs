using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetBooks;

public interface IGetBooksRepository
{
    Task<IEnumerable<BookDto>> GetAsync(CancellationToken cancellationToken = default);
}