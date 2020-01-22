using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesP2P.Data.Migrations
{
    public partial class AddedDescriptionColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Lectures");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lectures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lectures");

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
