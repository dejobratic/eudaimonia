using Eudaimonia.Application;
using Eudaimonia.Application.Books.AddBook;
using Eudaimonia.Application.Books.GetAllBooks;
using Eudaimonia.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    // TODO: Add some form of command/query dispatcher
    private readonly ICommandHandler<AddBookCommand> _addBookCommandHandler;
    private readonly IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>> _getAllBooksQueryHandler;

    public BooksController(
        ICommandHandler<AddBookCommand> addBookCommandHandler,
        IQueryHandler<GetAllBooksQuery, IEnumerable<BookDto>> getAllBooksQueryHandler)
    {
        _addBookCommandHandler = addBookCommandHandler;
        _getAllBooksQueryHandler = getAllBooksQueryHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(AddBookCommand command)
    {
        var result = await _addBookCommandHandler.HandleAsync(command);
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var query = new GetAllBooksQuery();
        var books = await _getAllBooksQueryHandler.HandleAsync(query);

        return Ok(books);
    }
}
