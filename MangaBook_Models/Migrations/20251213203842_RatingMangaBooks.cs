using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaBook_Models.Migrations
{
    /// <inheritdoc />
    public partial class RatingMangaBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "MangaBooks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "MangaBooks");
        }
    }
}
