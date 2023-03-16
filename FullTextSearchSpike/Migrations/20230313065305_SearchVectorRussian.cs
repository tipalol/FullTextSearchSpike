using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace FullTextSearchSpike.Migrations
{
    /// <inheritdoc />
    public partial class SearchVectorRussian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "search_vector",
                table: "books",
                type: "tsvector",
                nullable: false,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector")
                .Annotation("Npgsql:TsVectorConfig", "russian")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name", "content" })
                .OldAnnotation("Npgsql:TsVectorConfig", "english")
                .OldAnnotation("Npgsql:TsVectorProperties", new[] { "name", "content" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<NpgsqlTsVector>(
                name: "search_vector",
                table: "books",
                type: "tsvector",
                nullable: false,
                oldClrType: typeof(NpgsqlTsVector),
                oldType: "tsvector")
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name", "content" })
                .OldAnnotation("Npgsql:TsVectorConfig", "russian")
                .OldAnnotation("Npgsql:TsVectorProperties", new[] { "name", "content" });
        }
    }
}
