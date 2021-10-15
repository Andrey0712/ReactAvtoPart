using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Addfoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUser",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUser",
                table: "AspNetUsers");
        }
    }
}
