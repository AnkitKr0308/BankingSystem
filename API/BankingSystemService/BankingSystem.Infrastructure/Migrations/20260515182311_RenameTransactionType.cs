using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "TransactionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "Type");
        }
    }
}
