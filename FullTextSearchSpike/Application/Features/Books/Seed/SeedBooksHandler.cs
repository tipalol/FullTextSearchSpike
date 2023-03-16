using FullTextSearchSpike.Domain;
using FullTextSearchSpike.Infrastructure;
using MediatR;

namespace FullTextSearchSpike.Application.Features.Books.Seed;

public class SeedBooksHandler : IRequestHandler<SeedBooksCommand>
{
    private readonly SeedHelper _seedHelper;
    private readonly TextDbContext _dbContext;

    public SeedBooksHandler(SeedHelper seedHelper, TextDbContext dbContext)
    {
        _seedHelper = seedHelper;
        _dbContext = dbContext;
    }
    
    public async Task Handle(SeedBooksCommand command, CancellationToken cancellationToken)
    {
        var books = new List<Book>();

        for (var i = 0; i < command.Count; i++)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = _seedHelper.GetBookPhrase().Split(' ')[1],
                Content = _seedHelper.GetBookPhrase()
            };
            
            books.Add(book);
        }
        
        await _dbContext.Books.AddRangeAsync(books);
        await _dbContext.SaveChangesAsync();
    }
}