using Eudaimonia.Application.Utils;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
{
    private readonly IAuthorFactory<AddAuthorCommand> _authorFactory;
    private readonly IAddAuthorRepository _authorRepository;

    public AddAuthorCommandHandler(
        IAuthorFactory<AddAuthorCommand> authorFactory,
        IAddAuthorRepository authorRepository)
    {
        _authorFactory = authorFactory;
        _authorRepository = authorRepository;
    }

    public async Task<CommandResult> HandleAsync(AddAuthorCommand command)
    {
        var author = _authorFactory.CreateFrom(command);
        await _authorRepository.AddAsync(author);

        return new CommandResult(author.Id.ToString());
    }
}