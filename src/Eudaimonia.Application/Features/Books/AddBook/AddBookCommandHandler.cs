using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application.Features.Books.AddBook;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookFactory<AddBookCommand> _bookFactory;
    private readonly IAddBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(
        IBookFactory<AddBookCommand> bookFactory,
        IAddBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _bookFactory = bookFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddBookCommand command)
    {
        // TODO: How to validate the command, with the same validation rules as the Book entity?
        // Same as in FluentValidation, but without 3rd party libraries.
        // TODO: Include cancellation token.
        var book = _bookFactory.CreateFrom(command);

        await _bookRepository.AddAsync(book);
        await _unitOfWork.CommitAsync();

        return new CommandResult(book.Id.ToString());
    }
}