using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_InsuranceId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Advance",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillNumber",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DoctorCharge",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "LabCharge",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "MedicineCharge",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "OperationCharge",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "RoomCharge",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "TotalBill",
                table: "Bills",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "NursingCharge",
                table: "Bills",
                newName: "AppointmentId");

            migrationBuilder.AlterColumn<string>(
                name: "TestResult",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceId",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AppointmentId",
                table: "Bills",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DoctorId",
                table: "Bills",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_DoctorId",
                table: "Bills",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_InsuranceId",
                table: "Bills",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_DoctorId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_InsuranceId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AppointmentId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_DoctorId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Bills",
                newName: "NursingCharge");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Bills",
                newName: "TotalBill");

            migrationBuilder.AlterColumn<string>(
                name: "TestResult",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Advance",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BillNumber",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorCharge",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LabCharge",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MedicineCharge",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OperationCharge",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomCharge",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_InsuranceId",
                table: "Bills",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
