using Eudaimonia.Domain;

namespace Eudaimonia.Application.Books.AddPublisher;

public interface IAddPublisherRepository
{
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default);
}
