using FullTextSearchSpike.Application.Features.Search;
using FullTextSearchSpike.Application.Features.Search.Models;
using FullTextSearchSpike.Application.Features.Test;
using FullTextSearchSpike.Application.Features.Test.Models;
using FullTextSearchSpike.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchSpike.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly TextDbContext _dbContext;
    private readonly SeedHelper _seedHelper;
    private readonly IMediator _mediator;

    public SearchController(TextDbContext dbContext, SeedHelper seedHelper, IMediator mediator)
    {
        _dbContext = dbContext;
        _seedHelper = seedHelper;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<SearchResult>> VectorTextSearch([FromQuery] SearchQuery searchQuery)
    {
        return Ok(await _mediator.Send(searchQuery));
    }
    
    [HttpGet("test")]
    public async Task<ActionResult<SearchTestResult>> TestSearchMethods()
    {
        return Ok(await _mediator.Send(new SearchTestCommand()));
    }
}