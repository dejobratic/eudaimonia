using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public class AddPublisherCommandPublisherFactory : IPublisherFactory<AddPublisherCommand>
{
    public Publisher CreateFrom(AddPublisherCommand command)
        => new(
            new Text(command.FullName!),
            string.IsNullOrEmpty(command.Bio) ? null : new Text(command.Bio!));
}