using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientReport_AspNetUsers_DoctorId",
                table: "PatientReport");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReport_AspNetUsers_PatientId",
                table: "PatientReport");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedicines_PatientReport_PatientReportId",
                table: "PrescribedMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientReport",
                table: "PatientReport");

            migrationBuilder.RenameTable(
                name: "PatientReport",
                newName: "PatientReports");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReport_PatientId",
                table: "PatientReports",
                newName: "IX_PatientReports_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReport_DoctorId",
                table: "PatientReports",
                newName: "IX_PatientReports_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientReports",
                table: "PatientReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_AspNetUsers_DoctorId",
                table: "PatientReports",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_AspNetUsers_PatientId",
                table: "PatientReports",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedicines_PatientReports_PatientReportId",
                table: "PrescribedMedicines",
                column: "PatientReportId",
                principalTable: "PatientReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_AspNetUsers_DoctorId",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_AspNetUsers_PatientId",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedicines_PatientReports_PatientReportId",
                table: "PrescribedMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientReports",
                table: "PatientReports");

            migrationBuilder.RenameTable(
                name: "PatientReports",
                newName: "PatientReport");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReports_PatientId",
                table: "PatientReport",
                newName: "IX_PatientReport_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReports_DoctorId",
                table: "PatientReport",
                newName: "IX_PatientReport_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientReport",
                table: "PatientReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReport_AspNetUsers_DoctorId",
                table: "PatientReport",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReport_AspNetUsers_PatientId",
                table: "PatientReport",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedicines_PatientReport_PatientReportId",
                table: "PrescribedMedicines",
                column: "PatientReportId",
                principalTable: "PatientReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
