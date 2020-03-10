using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesP2P.Data.Migrations
{
    public partial class AddPaymentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");
        }
    }
}
