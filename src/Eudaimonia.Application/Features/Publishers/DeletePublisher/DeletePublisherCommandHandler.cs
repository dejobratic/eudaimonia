using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Publishers.DeletePublisher;

public class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IUnitOfWork unitOfWork)
    {
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(DeletePublisherCommand command, CancellationToken cancellationToken = default)
    {
        var publisher = await _publisherRepository.GetByIdAsync(new PublisherId(command.Id), cancellationToken);
        await _publisherRepository.DeleteAsync(publisher, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult();
    }
}