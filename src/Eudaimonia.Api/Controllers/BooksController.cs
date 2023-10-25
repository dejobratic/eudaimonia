using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public BooksController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var query = new GetAllBooksQuery();
        var books = await _queryDispatcher.DispatchAsync<IEnumerable<BookDto>>(query);

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(AddBookCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result.Data);
    }
}