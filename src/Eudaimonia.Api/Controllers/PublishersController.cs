using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Queries;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public PublishersController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllPublishersAsync()
    {
        var query = new GetAllPublishersQuery();
        var authors = await _queryDispatcher.DispatchAsync<IEnumerable<PublisherDto>>(query);

        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisherAsync(AddPublisherCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result.Data);
    }
}
