using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Features.Books.AddBook;
using Eudaimonia.Application.Features.Books.GetBookById;
using Eudaimonia.Application.Features.Books.GetBooks;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eudaimonia.Presentation.OData.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ODataController
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
        var query = new GetBookByIdQuery { Id = id };
        var book = await _queryDispatcher.DispatchAsync<BookDto>(query);
        

        return Ok(book);
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var query = new GetBooksQuery();
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