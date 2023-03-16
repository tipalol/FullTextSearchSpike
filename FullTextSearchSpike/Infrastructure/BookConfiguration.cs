using FullTextSearchSpike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullTextSearchSpike.Infrastructure;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(a => a.Content)
            .IsRequired();

        builder.HasGeneratedTsVectorColumn(
            b => b.SearchVector,
            "russian",
            b => new {b.Name, b.Content});

        builder.HasIndex(b => b.SearchVector).HasMethod("GIN");
    }
}