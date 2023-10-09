using Eudaimonia.Application;
using Eudaimonia.Application.Books.AddAuthor;
using Microsoft.AspNetCore.Mvc;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    // TODO: Add some form of command/query dispatcher
    private readonly ICommandHandler<AddAuthorCommand> _addAuthorCommandHandler;

    public AuthorsController(
        ICommandHandler<AddAuthorCommand> addAuthorCommandHandler)
    {
        _addAuthorCommandHandler = addAuthorCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync(AddAuthorCommand command)
    {
        var result = await _addAuthorCommandHandler.HandleAsync(command);
        return Ok(result.Data);
    }
}
