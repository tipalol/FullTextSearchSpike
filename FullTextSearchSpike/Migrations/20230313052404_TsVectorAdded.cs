using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace FullTextSearchSpike.Migrations
{
    /// <inheritdoc />
    public partial class TsVectorAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "search_vector",
                table: "books",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name", "content" });

            migrationBuilder.CreateIndex(
                name: "ix_books_search_vector",
                table: "books",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_books_search_vector",
                table: "books");

            migrationBuilder.DropColumn(
                name: "search_vector",
                table: "books");
        }
    }
}
