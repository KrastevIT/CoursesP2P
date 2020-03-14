using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesP2P.Data.Migrations
{
    public partial class AddColumnVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vote",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vote",
                table: "Ratings");
        }
    }
}
