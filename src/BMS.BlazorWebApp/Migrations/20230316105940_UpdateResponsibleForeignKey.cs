using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.BlazorWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResponsibleForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Responsible1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Responsible2",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Responsible1Id",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible2Id",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Responsible1Id",
                table: "Tasks",
                column: "Responsible1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Responsible2Id",
                table: "Tasks",
                column: "Responsible2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Responsible1Id",
                table: "Tasks",
                column: "Responsible1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Responsible2Id",
                table: "Tasks",
                column: "Responsible2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Responsible1Id",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Responsible2Id",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Responsible1Id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Responsible2Id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Responsible1Id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Responsible2Id",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible1",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible2",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
