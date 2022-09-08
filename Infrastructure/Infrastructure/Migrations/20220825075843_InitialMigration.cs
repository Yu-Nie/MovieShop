using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    Tagline = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 9.9m),
                    Revenue = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 9.9m),
                    ImdbUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    TmdbUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    BackdropUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunTime = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 9.9m),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Budget",
                table: "Movies",
                column: "Budget");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Price",
                table: "Movies",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Revenue",
                table: "Movies",
                column: "Revenue");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
