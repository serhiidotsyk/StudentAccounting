using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addedendandstartdatetoUserCoursetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledJob_Users_UserId",
                table: "ScheduledJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduledJob",
                table: "ScheduledJob");

            migrationBuilder.RenameTable(
                name: "ScheduledJob",
                newName: "ScheduledJobs");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledJob_UserId",
                table: "ScheduledJobs",
                newName: "IX_ScheduledJobs_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserCourses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserCourses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduledJobs",
                table: "ScheduledJobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledJobs_Users_UserId",
                table: "ScheduledJobs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledJobs_Users_UserId",
                table: "ScheduledJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduledJobs",
                table: "ScheduledJobs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UserCourses");

            migrationBuilder.RenameTable(
                name: "ScheduledJobs",
                newName: "ScheduledJob");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledJobs_UserId",
                table: "ScheduledJob",
                newName: "IX_ScheduledJob_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduledJob",
                table: "ScheduledJob",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledJob_Users_UserId",
                table: "ScheduledJob",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
