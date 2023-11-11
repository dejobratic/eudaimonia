using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Application.Utils.Repositories;

namespace Eudaimonia.Application.Features.Books.GetBookById;

public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookDtoRepository _bookRepository;

    public GetBookByIdQueryHandler(IBookDtoRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> HandleAsync(GetBookByIdQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        return await _bookRepository.GetByIdAsync(query.Id, cancellationToken);
    }
}