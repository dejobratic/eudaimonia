using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetPublishers;

public interface IGetPublishersRepository
{
    Task<IEnumerable<PublisherDto>> GetAsync(CancellationToken cancellationToken = default);
}