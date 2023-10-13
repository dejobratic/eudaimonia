using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public class AddPublisherCommandHandler : ICommandHandler<AddPublisherCommand>
{
    private readonly IPublisherFactory<AddPublisherCommand> _publisherFactory;
    private readonly IAddPublisherRepository _publisherRepository;

    public AddPublisherCommandHandler(
        IPublisherFactory<AddPublisherCommand> publisherFactory,
        IAddPublisherRepository publisherRepository)
    {
        _publisherFactory = publisherFactory;
        _publisherRepository = publisherRepository;
    }

    public async Task<CommandResult> HandleAsync(AddPublisherCommand command)
    {
        var publisher = _publisherFactory.CreateFrom(command);
        await _publisherRepository.AddAsync(publisher);

        return new CommandResult(publisher.Id.ToString());
    }
}