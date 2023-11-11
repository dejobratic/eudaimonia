using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Eudaimonia.Application.Utils.Repositories;

namespace Eudaimonia.Application.Features.Publishers.GetPublishers;

public class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, IEnumerable<PublisherDto>>
{
    private readonly IPublisherDtoRepository _publisherRepository;

    public GetPublishersQueryHandler(IPublisherDtoRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<PublisherDto>> HandleAsync(GetPublishersQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: include specification from query
        return await _publisherRepository.GetAsync(null!, cancellationToken);
    }
}