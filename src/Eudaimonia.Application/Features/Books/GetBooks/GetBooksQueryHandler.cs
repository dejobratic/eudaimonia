using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Application.Utils.Repositories;

namespace Eudaimonia.Application.Features.Books.GetBooks;

public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IBookDtoRepository _bookRepository;

    public GetBooksQueryHandler(IBookDtoRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> HandleAsync(GetBooksQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: include predicate from query
        return await _bookRepository.GetAsync(null!, cancellationToken);
    }
}