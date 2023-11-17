using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriveLink",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_CourseId",
                table: "OrderHeaders",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Courses_CourseId",
                table: "OrderHeaders",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Courses_CourseId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_CourseId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "DriveLink",
                table: "Courses");
        }
    }
}
