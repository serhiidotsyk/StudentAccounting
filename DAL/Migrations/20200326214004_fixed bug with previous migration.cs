using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fixedbugwithpreviousmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Users_UserId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCourses");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_StudentId",
                table: "UserCourses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Users_StudentId",
                table: "UserCourses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Users_StudentId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_StudentId",
                table: "UserCourses");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Users_UserId",
                table: "UserCourses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
