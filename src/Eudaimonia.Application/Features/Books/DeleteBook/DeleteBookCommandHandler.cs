using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.DeleteBook;

public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(DeleteBookCommand command, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetByIdAsync(new BookId(command.Id), cancellationToken);
        await _bookRepository.DeleteAsync(book, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult();
    }
}