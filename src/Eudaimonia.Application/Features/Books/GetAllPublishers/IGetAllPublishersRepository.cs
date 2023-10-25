using Eudaimonia.Application.Utils.Dtos;

namespace Eudaimonia.Application.Features.Books.GetAllPublishers;

public interface IGetAllPublishersRepository
{
    Task<IEnumerable<PublisherDto>> GetAllAsync(CancellationToken cancellationToken = default);
}