using System.Text.Json.Serialization;
using FullTextSearchSpike.Application.Features.Search.Models;
using FullTextSearchSpike.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearchSpike.Application.Features.Search;

public class SearchQuery : IRequest<SearchResult>
{
    public string Query { get; set; }
    
    public int Take { get; set; } 
}