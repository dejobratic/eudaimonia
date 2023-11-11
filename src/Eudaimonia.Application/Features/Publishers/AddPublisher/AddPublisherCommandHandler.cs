using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Factories;

namespace Eudaimonia.Application.Features.Publishers.AddPublisher;

public class AddPublisherCommandHandler : ICommandHandler<AddPublisherCommand>
{
    private readonly IPublisherFactory _publisherFactory;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPublisherCommandHandler(
        IPublisherFactory publisherFactory,
        IPublisherRepository publisherRepository,
        IUnitOfWork unitOfWork)
    {
        _publisherFactory = publisherFactory;
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddPublisherCommand command, CancellationToken cancellationToken = default)
    {
        var publisher = CreateBookFrom(command);

        await _publisherRepository.AddAsync(publisher, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult(publisher.Id.ToString());
    }

    private Publisher CreateBookFrom(AddPublisherCommand command)
        => _publisherFactory.Create(command.FullName!, command.Bio);
}