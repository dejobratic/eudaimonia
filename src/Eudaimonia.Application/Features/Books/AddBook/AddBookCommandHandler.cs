using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application.Features.Books.AddBook;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookFactory<AddBookCommand> _bookFactory;
    private readonly IAddBookRepository _bookRepository;

    public AddBookCommandHandler(
        IBookFactory<AddBookCommand> bookFactory,
        IAddBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        _bookFactory = bookFactory;
    }

    public async Task<CommandResult> HandleAsync(AddBookCommand command)
    {
        // TODO: How to validate the command, with the same validation rules as the Book entity?
        // Same as in FluentValidation, but without 3rd party libraries.
        // TODO: Include cancellation token.
        var book = _bookFactory.CreateFrom(command);
        await _bookRepository.AddAsync(book);

        return new CommandResult(book.Id.ToString());
    }
}