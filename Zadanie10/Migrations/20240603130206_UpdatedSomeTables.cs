using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zadanie10.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "fk@gmail.com", "Filip", "Kowalski" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "jp@gmail.com", "Joanna", "Pietruszka" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Type" },
                values: new object[] { "OPIS_A", "AAA", "TYP_A" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Type" },
                values: new object[] { "OPIS_B", "BBB", "TYP_B" });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(1996, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2024, 6, 3, 15, 2, 6, 14, DateTimeKind.Local).AddTicks(3715), new DateTime(2024, 6, 13, 15, 2, 6, 14, DateTimeKind.Local).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2024, 6, 3, 15, 2, 6, 14, DateTimeKind.Local).AddTicks(3775), new DateTime(2024, 6, 18, 15, 2, 6, 14, DateTimeKind.Local).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 },
                column: "Details",
                value: "Codziennie po pierwszym posiłku");

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 },
                column: "Details",
                value: "Codziennie przed spaniem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "john.doe@example.com", "John", "Doe" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "jane.smith@example.com", "Jane", "Smith" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Type" },
                values: new object[] { "DescriptionA", "MedicamentA", "TypeA" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Type" },
                values: new object[] { "DescriptionB", "MedicamentB", "TypeB" });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "Birthdate",
                value: new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "Birthdate",
                value: new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2024, 6, 3, 13, 45, 30, 252, DateTimeKind.Local).AddTicks(2642), new DateTime(2024, 6, 13, 13, 45, 30, 252, DateTimeKind.Local).AddTicks(2693) });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2024, 6, 3, 13, 45, 30, 252, DateTimeKind.Local).AddTicks(2698), new DateTime(2024, 6, 18, 13, 45, 30, 252, DateTimeKind.Local).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 },
                column: "Details",
                value: "Take after meal");

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 },
                column: "Details",
                value: "Take before bed");
        }
    }
}
