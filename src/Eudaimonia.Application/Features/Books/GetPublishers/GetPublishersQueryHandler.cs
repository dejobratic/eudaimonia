using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Application.Features.Books.GetPublishers;

public class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, IEnumerable<PublisherDto>>
{
    private readonly IGetPublishersRepository _publisherRepository;

    public GetPublishersQueryHandler(IGetPublishersRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<PublisherDto>> HandleAsync(GetPublishersQuery query)
    {
        // TODO: Need to return paginated results, sorting and filtering.
        // TODO: Include cancellation token.
        return await _publisherRepository.GetAsync();
    }
}