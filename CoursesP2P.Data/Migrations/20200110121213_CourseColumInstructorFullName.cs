using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesP2P.Data.Migrations
{
    public partial class CourseColumInstructorFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructorFullName",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorFullName",
                table: "Courses");
        }
    }
}
