using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetBookById;

public interface IGetBookByIdRepository
{
    Task<BookDto> GetById(Guid id, CancellationToken cancellationToken = default);
}