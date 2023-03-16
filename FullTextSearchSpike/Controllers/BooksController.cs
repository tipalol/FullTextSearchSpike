using FullTextSearchSpike.Application.Features.Books.Add;
using FullTextSearchSpike.Application.Features.Books.Seed;
using FullTextSearchSpike.Domain;
using FullTextSearchSpike.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;

namespace FullTextSearchSpike.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly TextDbContext _dbContext;
    private readonly SeedHelper _seedHelper;
    private readonly IMediator _mediator;

    public BooksController(TextDbContext dbContext, SeedHelper seedHelper, IMediator mediator)
    {
        _dbContext = dbContext;
        _seedHelper = seedHelper;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получает список книг
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Book>> Get([FromQuery] int take = 10)
    {
        var books = _dbContext.Books.Take(take).ToArray();
        return Ok(books);
    }
    
    /// <summary>
    /// Получает TsVector 
    /// </summary>
    [HttpGet("vector")]
    public ActionResult<NpgsqlTsVector> GetTsVector([FromQuery] Guid id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

        return Ok(book?.SearchVector);
    }
    
    /// <summary>
    /// Считает кол-во книг в бд
    /// </summary>
    [HttpGet("count")]
    public ActionResult<int> Count()
    {
        var books = _dbContext.Books.Count();
        return Ok(books);
    }
    
    /// <summary>
    /// Добавляет в бд книги из файла Books.json
    /// </summary>
    [HttpGet("file")]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        var books = await _seedHelper.ReadBooksFile();
        return Ok(books);
    }

    /// <summary>
    /// Ручное добавление книги
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Book>> Add([FromBody] AddBookCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    /// <summary>
    /// Генерация книг в бд
    /// </summary>
    [HttpPost("{count}")]
    public async Task<ActionResult<Book>> Seed([FromRoute] int count = 500)
    {
        await _mediator.Send(new SeedBooksCommand() {Count = count});
        return Ok();
    }
    
    /// <summary>
    /// Генерация книг в файл
    /// </summary>
    [HttpPost("file/{count}")]
    public async Task<ActionResult<Book>> SeedFile([FromRoute] int count = 500)
    {
        await _seedHelper.GenerateBooksFile(count);
        return Ok();
    }

    /// <summary>
    /// Очистка бд
    /// </summary>
    [HttpDelete]
    public ActionResult Clear()
    {
        _dbContext.Books.RemoveRange(_dbContext.Books);
        _dbContext.SaveChanges();
        return Ok();
    }
}