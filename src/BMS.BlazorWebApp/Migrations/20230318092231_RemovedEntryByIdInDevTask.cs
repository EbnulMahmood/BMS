using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.BlazorWebApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedEntryByIdInDevTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_EntryById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EntryById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EntryById",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryById",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EntryById",
                table: "Tasks",
                column: "EntryById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_EntryById",
                table: "Tasks",
                column: "EntryById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
