using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetAuthors;

public class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IGetAuthorsRepository _authorRepository;

    public GetAuthorsQueryHandler(IGetAuthorsRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> HandleAsync(GetAuthorsQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _authorRepository.GetAsync();
    }
}