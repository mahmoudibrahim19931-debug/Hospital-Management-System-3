using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLabs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Labs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Labs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Labs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResultDate",
                table: "Labs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labs_DoctorId",
                table: "Labs",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_AspNetUsers_DoctorId",
                table: "Labs",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_AspNetUsers_DoctorId",
                table: "Labs");

            migrationBuilder.DropIndex(
                name: "IX_Labs_DoctorId",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "ResultDate",
                table: "Labs");
        }
    }
}
