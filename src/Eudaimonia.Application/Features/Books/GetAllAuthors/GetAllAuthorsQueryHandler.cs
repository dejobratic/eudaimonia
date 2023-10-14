using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Extensions;

namespace Eudaimonia.Application.Features.Books.GetAllAuthors;

public class GetAllAuthorsQueryHandler : IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IGetAllAuthorsRepository _authorRepository;

    public GetAllAuthorsQueryHandler(IGetAllAuthorsRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> HandleAsync(GetAllAuthorsQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _authorRepository.GetAllAsync();
    }
}