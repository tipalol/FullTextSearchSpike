using FullTextSearchSpike.Domain;
using FullTextSearchSpike.Infrastructure;
using MediatR;

namespace FullTextSearchSpike.Application.Features.Books.Add;

public class AddBookHandler : IRequestHandler<AddBookCommand, Book>
{
    private readonly TextDbContext _dbContext;

    public AddBookHandler(TextDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Book> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Content = command.Content
        };

        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book;
    }
}