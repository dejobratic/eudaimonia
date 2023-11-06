using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetPublishers;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Dtos;
using Eudaimonia.Application.Utils.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eudaimonia.Presentation.OData.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ODataController
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
        var query = new GetPublishersQuery();
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