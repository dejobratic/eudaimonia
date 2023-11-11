using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Application.Utils.Repositories;

namespace Eudaimonia.Application.Features.Authors.GetAuthors;

public class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAuthorDtoRepository _authorRepository;

    public GetAuthorsQueryHandler(IAuthorDtoRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> HandleAsync(GetAuthorsQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: include predicate from query
        return await _authorRepository.GetAsync(null, cancellationToken);
    }
}