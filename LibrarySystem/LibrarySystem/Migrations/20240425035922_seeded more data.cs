using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class seededmoredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patrons",
                keyColumn: "PatronId",
                keyValue: 1,
                column: "Address",
                value: "123 Red Street");

            migrationBuilder.InsertData(
                table: "Patrons",
                columns: new[] { "PatronId", "Address", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 2, "456 Orange Street", "Jack", "Smith", "5135484456" },
                    { 3, "789 Yellow Street", "Purple", "Poo", "5135564897" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "BookId", "DateBorrowed", "PatronId", "ReturnDate", "TotalDaysAllowedToBorrow" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 100 },
                    { 3, 3, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patrons",
                keyColumn: "PatronId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patrons",
                keyColumn: "PatronId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Patrons",
                keyColumn: "PatronId",
                keyValue: 1,
                column: "Address",
                value: "1225 Green Street");
        }
    }
}
