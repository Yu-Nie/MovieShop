using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class recreateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                   LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                   DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                   Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                   HashedPassword = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                   PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                   Salt = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                   IsLocked = table.Column<bool>(type: "bit", nullable: false),
                   ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Users", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
