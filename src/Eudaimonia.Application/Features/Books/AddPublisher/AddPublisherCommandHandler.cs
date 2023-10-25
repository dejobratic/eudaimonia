using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public class AddPublisherCommandHandler : ICommandHandler<AddPublisherCommand>
{
    private readonly IFactory<AddPublisherCommand, Publisher> _publisherFactory;
    private readonly IAddPublisherRepository _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPublisherCommandHandler(
        IFactory<AddPublisherCommand, Publisher> publisherFactory,
        IAddPublisherRepository publisherRepository,
        IUnitOfWork unitOfWork)
    {
        _publisherFactory = publisherFactory;
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddPublisherCommand command)
    {
        var publisher = _publisherFactory.CreateFrom(command);

        await _publisherRepository.AddAsync(publisher);
        await _unitOfWork.CommitAsync();

        return new CommandResult(publisher.Id.ToString());
    }
}