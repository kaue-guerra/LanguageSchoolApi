using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageSchoolApi.Migrations
{
    public partial class addCourseMatriculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Matriculates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculates_StudentId",
                table: "Matriculates",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculates_Students_StudentId",
                table: "Matriculates",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculates_Students_StudentId",
                table: "Matriculates");

            migrationBuilder.DropIndex(
                name: "IX_Matriculates_StudentId",
                table: "Matriculates");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Matriculates");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentId",
                table: "Courses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
