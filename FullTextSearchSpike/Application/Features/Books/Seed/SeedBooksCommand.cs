using MediatR;

namespace FullTextSearchSpike.Application.Features.Books.Seed;

public class SeedBooksCommand : IRequest
{
    public int Count { get; set; }
}