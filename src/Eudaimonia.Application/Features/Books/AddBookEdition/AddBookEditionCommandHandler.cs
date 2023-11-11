using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Factories;

namespace Eudaimonia.Application.Features.Books.AddBookEdition;

public class AddBookEditionCommandHandler : ICommandHandler<AddBookEditionCommand>
{
    private readonly IEditionFactory _editionFactory;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookEditionCommandHandler(
        IEditionFactory bookFactory,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _editionFactory = bookFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddBookEditionCommand command, CancellationToken cancellationToken = default)
    {
        var book = await GetBookAsync(command.BookId, cancellationToken);

        var edition = CreateEditionFrom(command);
        book.AddEdition(edition);

        await _bookRepository.UpdateAsync(book, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult(book.Id.ToString());
    }

    private async Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken)
    {
        var bookId = new BookId(id);
        return await _bookRepository.GetByIdAsync(bookId, cancellationToken);
    }

    private Edition CreateEditionFrom(AddBookEditionCommand command)
        => _editionFactory.Create(
            command.Edition?.Title!,
            command.Edition?.Description!,
            command.Edition?.Language!,
            command.Edition?.Specs?.PageCount ?? default,
            command.Edition?.Specs?.FrontCover?.Name!,
            command.Edition?.Specs?.FrontCover?.Url!,
            command.Edition?.Specs?.Format!,
            command.Edition?.PublisherId ?? Guid.Empty,
            command.Edition?.PublicationYear ?? default);
}