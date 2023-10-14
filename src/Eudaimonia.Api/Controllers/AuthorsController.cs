using Eudaimonia.Application.Features.Books.AddAuthor;
using Eudaimonia.Application.Features.Books.GetAllAuthors;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    // TODO: Add some form of command/query dispatcher
    private readonly IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>> _getAllAuthorsQueryHandler;
    private readonly ICommandHandler<AddAuthorCommand> _addAuthorCommandHandler;

    public AuthorsController(
        IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>> getAllAuthorsQueryHandler,
        ICommandHandler<AddAuthorCommand> addAuthorCommandHandler)
    {
        _getAllAuthorsQueryHandler = getAllAuthorsQueryHandler;
        _addAuthorCommandHandler = addAuthorCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var query = new GetAllAuthorsQuery();
        var authors = await _getAllAuthorsQueryHandler.HandleAsync(query);

        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync(AddAuthorCommand command)
    {
        var result = await _addAuthorCommandHandler.HandleAsync(command);
        return Ok(result.Data);
    }
}
