using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Authors.DeleteAuthor;

public class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(DeleteAuthorCommand command, CancellationToken cancellationToken = default)
    {
        var author = await _authorRepository.GetByIdAsync(new AuthorId(command.Id), cancellationToken);
        await _authorRepository.DeleteAsync(author, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult();
    }
}