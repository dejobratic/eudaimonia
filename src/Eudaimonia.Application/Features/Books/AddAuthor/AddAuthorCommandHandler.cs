﻿using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Domain;

namespace Eudaimonia.Application.Features.Books.AddAuthor;

public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
{
    private readonly IFactory<AddAuthorCommand, Author> _authorFactory;
    private readonly IAddAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorCommandHandler(
        IFactory<AddAuthorCommand, Author> authorFactory,
        IAddAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorFactory = authorFactory;
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> HandleAsync(AddAuthorCommand command)
    {
        var author = _authorFactory.CreateFrom(command);

        await _authorRepository.AddAsync(author);
        await _unitOfWork.CommitAsync();

        return new CommandResult(author.Id.ToString());
    }
}