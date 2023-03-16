using FullTextSearchSpike.Domain;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearchSpike.Infrastructure;

public class TextDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public TextDbContext(DbContextOptions<TextDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TextDbContext).Assembly);
    }
}