using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Factories;

namespace Eudaimonia.Application.Features.Authors.AddAuthor;

public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
{
    private readonly IAuthorFactory _authorFactory;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorCommandHandler(
        IAuthorFactory authorFactory,
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorFactory = authorFactory;
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddAuthorCommand command, CancellationToken cancellationToken = default)
    {
        var author = CreateBookFrom(command);

        await _authorRepository.AddAsync(author, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult(author.Id.ToString());
    }

    private Author CreateBookFrom(AddAuthorCommand command)
        => _authorFactory.Create(command.FullName!, command.Bio);
}