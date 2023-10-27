using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetBooks;

public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IGetBooksRepository _bookRepository;

    public GetBooksQueryHandler(IGetBooksRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> HandleAsync(GetBooksQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _bookRepository.GetAsync();
    }
}