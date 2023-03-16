using System.Text.Json.Serialization;
using FullTextSearchSpike.Domain;

namespace FullTextSearchSpike.Application.Features.Search.Models;

public class SearchResult
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("timeSpent")]
    public long TimeSpent { get; set; }
    
    [JsonPropertyName("result")]
    public IEnumerable<Book> Result { get; set; }
}