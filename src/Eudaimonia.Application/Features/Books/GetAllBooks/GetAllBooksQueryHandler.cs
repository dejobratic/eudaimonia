using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetAllBooks;

public class GetAllBooksQueryHandler : IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IGetAllBooksRepository _bookRepository;

    public GetAllBooksQueryHandler(IGetAllBooksRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> HandleAsync(GetAllBooksQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _bookRepository.GetAllAsync();
    }
}