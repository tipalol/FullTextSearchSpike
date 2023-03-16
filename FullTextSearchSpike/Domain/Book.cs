using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace FullTextSearchSpike.Domain;

public class Book : Entity
{
    public string Name { get; set; }
    
    public string Content { get; set; }
    
    [JsonIgnore]
    public NpgsqlTsVector SearchVector { get; set; }
}