using FullTextSearchSpike.Application.Features.Test.Models;
using FullTextSearchSpike.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchSpike.Application.Features.Test;

public class SearchTestHandler : IRequestHandler<SearchTestCommand, SearchTestResult>
{
    private readonly TextDbContext _dbContext;

    public SearchTestHandler(TextDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<SearchTestResult> Handle(SearchTestCommand request, CancellationToken cancellationToken)
    {
        var searchWord = "друг";
        
        var containsWatch = new System.Diagnostics.Stopwatch();
            
        containsWatch.Start();
        
        var containsResult = await _dbContext.Books
            .Where(x => x.Name.Contains(searchWord) || x.Content.Contains(searchWord))
            .ToListAsync(cancellationToken);
        
        containsWatch.Stop();
        
        var vectorWatch = new System.Diagnostics.Stopwatch();
        
        vectorWatch.Start();
        
        var vectorResult = await _dbContext.Books
            .Where(x => x.SearchVector.Matches(searchWord))
            .ToListAsync(cancellationToken);
        
        vectorWatch.Stop();

        return new SearchTestResult()
        {
            BooksFoundByContains = containsResult.Count,
            BooksFoundByTsVector = vectorResult.Count,
            TimeSpentOnContains = containsWatch.ElapsedMilliseconds,
            TimeSpentOnTsVector = vectorWatch.ElapsedMilliseconds
        };
    }
}