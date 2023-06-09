using System.Diagnostics;
using FullTextSearchSpike.Application.Features.Search.Models;
using FullTextSearchSpike.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchSpike.Application.Features.Search;

public class SearchHandler : IRequestHandler<SearchQuery, SearchResult>
{
    private readonly TextDbContext _dbContext;

    public SearchHandler(TextDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<SearchResult> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var watch = new Stopwatch();
        watch.Start();

        var books = await _dbContext.Books
            .AsNoTracking()
            .Where(x => x.SearchVector.Matches(request.Query))
            .Select(x => new BookModel(){Id = x.Id, Name = x.Name, Content = x.Content.Substring(0, 120)})
            .Take(request.Take)
            .ToArrayAsync(cancellationToken);

        watch.Stop();

        var result = new SearchResult()
        {
            Count = books.Length,
            TimeSpent = watch.ElapsedMilliseconds,
            Result = books//.Select(x => new BookModel(){Id = x.Id, Name = x.Name, Content = x.Content})
        };

        return result;
    }
}