using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetBookById;

public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, BookDto>
{
    private readonly IGetBookByIdRepository _bookRepository;

    public GetBookByIdQueryHandler(IGetBookByIdRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> HandleAsync(GetBookByIdQuery query)
    {
        return await _bookRepository.GetById(query.Id, CancellationToken.None);
    }
}