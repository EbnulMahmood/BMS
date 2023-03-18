using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.BlazorWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQAResponsibleIdForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QAResponsible",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "QAResponsibleId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_QAResponsibleId",
                table: "Tasks",
                column: "QAResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_QAResponsibleId",
                table: "Tasks",
                column: "QAResponsibleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_QAResponsibleId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_QAResponsibleId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "QAResponsibleId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "QAResponsible",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
