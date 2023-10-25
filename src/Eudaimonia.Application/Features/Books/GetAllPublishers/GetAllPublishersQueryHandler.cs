using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Extensions;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetAllPublishers;

public class GetAllPublishersQueryHandler : IQueryHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>>
{
    private readonly IGetAllPublishersRepository _publisherRepository;

    public GetAllPublishersQueryHandler(IGetAllPublishersRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<PublisherDto>> HandleAsync(GetAllPublishersQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _publisherRepository.GetAllAsync();
    }
}