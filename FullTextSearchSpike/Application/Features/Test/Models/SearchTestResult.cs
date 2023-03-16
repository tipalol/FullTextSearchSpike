namespace FullTextSearchSpike.Application.Features.Test.Models;

public class SearchTestResult
{
    public int BooksFoundByContains { get; set; }
    public long TimeSpentOnContains { get; set; }
    
    public int BooksFoundByTsVector { get; set; }
    public long TimeSpentOnTsVector { get; set; }
}