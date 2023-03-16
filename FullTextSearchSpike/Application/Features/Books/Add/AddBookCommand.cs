using FullTextSearchSpike.Domain;
using MediatR;

namespace FullTextSearchSpike.Application.Features.Books.Add;

public class AddBookCommand : IRequest<Book>
{
    public string Name { get; set; }
    
    public string Content { get; set; }
}