using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InsuranceDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Insurances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Insurances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalAmount",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InsuranceId",
                table: "AspNetUsers",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Insurances_InsuranceId",
                table: "AspNetUsers",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Insurances_InsuranceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InsuranceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "OriginalAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "AspNetUsers");
        }
    }
}
