﻿using Eudaimonia.Application.Dtos;
using Eudaimonia.Application.Features.Authors.AddAuthor;
using Eudaimonia.Application.Features.Authors.DeleteAuthor;
using Eudaimonia.Application.Features.Authors.GetAuthors;
using Eudaimonia.Application.Utils.Commands;
using Eudaimonia.Application.Utils.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eudaimonia.Presentation.OData.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ODataController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthorsController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var query = new GetAuthorsQuery();
        var authors = await _queryDispatcher.DispatchAsync<IEnumerable<AuthorDto>>(query);

        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync(AddAuthorCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id)
    {
        var command = new DeleteAuthorCommand { Id = id };
        await _commandDispatcher.DispatchAsync(command);
        
        return Ok();
    }
}