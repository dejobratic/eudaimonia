using Eudaimonia.Application.Features.Books.AddPublisher;
using Eudaimonia.Application.Features.Books.GetAllPublishers;
using Eudaimonia.Application.Features.Books.GetAllBooks;
using Eudaimonia.Application.Utils;
using Eudaimonia.Application.Utils.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Eudaimonia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    // TODO: Add some form of command/query dispatcher
    private readonly IQueryHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>> _getAllPublishersQueryHandler;
    private readonly ICommandHandler<AddPublisherCommand> _addPublisherCommandHandler;

    public PublishersController(
        IQueryHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>> getAllPublishersQueryHandler,
        ICommandHandler<AddPublisherCommand> addPublisherCommandHandler)
    {
        _getAllPublishersQueryHandler = getAllPublishersQueryHandler;
        _addPublisherCommandHandler = addPublisherCommandHandler;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllPublishersAsync()
    {
        var query = new GetAllPublishersQuery();
        var authors = await _getAllPublishersQueryHandler.HandleAsync(query);

        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisherAsync(AddPublisherCommand command)
    {
        var result = await _addPublisherCommandHandler.HandleAsync(command);
        return Ok(result.Data);
    }
}
