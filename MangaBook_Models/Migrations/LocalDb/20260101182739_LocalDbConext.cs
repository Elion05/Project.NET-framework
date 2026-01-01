using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaBook_Models.Migrations.LocalDb
{
    /// <inheritdoc />
    public partial class LocalDbConext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nieuws_Berichten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titel = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Inhoud = table.Column<string>(type: "TEXT", maxLength: 3500, nullable: false),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GebruikerId = table.Column<string>(type: "TEXT", nullable: false),
                    isVerwijderd = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nieuws_Berichten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nieuws_Berichten_MangaUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "MangaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nieuws_Berichten_GebruikerId",
                table: "Nieuws_Berichten",
                column: "GebruikerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nieuws_Berichten");
        }
    }
}
