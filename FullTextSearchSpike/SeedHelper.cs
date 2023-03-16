using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FullTextSearchSpike.Domain;
using FullTextSearchSpike.Infrastructure;

namespace FullTextSearchSpike;

public class SeedHelper
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Random random = new Random();
    private readonly string[] _bookLines;

    private const string BooksFilePath = "Utils/Books.json";

    public SeedHelper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _bookLines = File.ReadAllLines("Utils/War.txt", Encoding.Default);
    }
    
    public string GetBookPhrase()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < 8; i++)
            sb.AppendLine(_bookLines[random.Next(_bookLines.Length - 1)] + " ");

        return sb.ToString();
    }

    public async Task<int> ReadBooksFile()
    {
        if (File.Exists(BooksFilePath) is false)
            return 0;
        
        var fileLines = await File.ReadAllLinesAsync(BooksFilePath);
        var count = 0;
        
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TextDbContext>();

        foreach (var content in fileLines)
        {
            var books = JsonSerializer.Deserialize<List<Book>>(content);

            if (books != null)
            {
                await dbContext.Books.AddRangeAsync(books);
                count += books.Count;
            }

            await dbContext.SaveChangesAsync();
        }

        return count;
    }

    public async Task GenerateBooksFile(int count)
    {
        const int BatchSize = 2000;
        var books = new List<Book>();
        
        File.Delete(BooksFilePath);

        for (var i = 0; i < count / BatchSize; i++)
        {
            HandleBatch(books, BatchSize);
            
            await File.AppendAllLinesAsync(BooksFilePath, new []{JsonSerializer.Serialize(books)});
            books.Clear();
        }
    }

    private void HandleBatch(ICollection<Book> books, int size)
    {
        for (var i = 0; i < size; i++)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = GetBookPhrase().Split(' ')[1],
                Content = GetBookPhrase()
            };
            
            books.Add(book);
        }
    }
}