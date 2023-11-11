using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Repositories;
using Eudaimonia.Domain;
using Eudaimonia.Domain.Factories;

namespace Eudaimonia.Application.Features.Books.AddBook;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookFactory _bookFactory;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(
        IBookFactory bookFactory,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _bookFactory = bookFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddBookCommand command, CancellationToken cancellationToken = default)
    {
        var book = CreateBookFrom(command);

        await _bookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CommandResult(book.Id.ToString());
    }

    private Book CreateBookFrom(AddBookCommand command)
        => _bookFactory.Create(
            command.OriginalTitle!,
            command.OriginalLanguage!,
            command.AuthorId ?? Guid.Empty,
            command.Genres,
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