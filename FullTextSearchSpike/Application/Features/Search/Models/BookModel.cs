using FullTextSearchSpike.Domain;

namespace FullTextSearchSpike.Application.Features.Search.Models;

public class BookModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Content { get; set; }

    public static BookModel MapFrom(Book book)
    {
        return new BookModel()
        {
            Id = book.Id,
            Name = book.Name,
            Content = book.Content
        };
    }
}