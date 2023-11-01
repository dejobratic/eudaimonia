using Eudaimonia.Application.Utils;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public class AddPublisherCommandPublisherFactory : IFactory<AddPublisherCommand, Publisher>
{
    public Publisher CreateFrom(AddPublisherCommand command)
        => new(
            new PublisherId(),
            new Text(command.FullName!),
            string.IsNullOrEmpty(command.Bio) ? null : new Text(command.Bio!));
}