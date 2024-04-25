using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class addtransactionseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "BookId", "DateBorrowed", "PatronId", "ReturnDate", "TotalDaysAllowedToBorrow" },
                values: new object[] { 1, null, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1);
        }
    }
}
