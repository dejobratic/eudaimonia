using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public interface IAddPublisherRepository
{
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default);
}