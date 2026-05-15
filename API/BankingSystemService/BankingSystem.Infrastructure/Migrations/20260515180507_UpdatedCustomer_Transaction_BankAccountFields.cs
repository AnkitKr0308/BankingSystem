using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomer_Transaction_BankAccountFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
       name: "Type_New",
       table: "Transactions",
       nullable: false,
       defaultValue: 0);

            migrationBuilder.Sql(@"
        UPDATE Transactions
        SET Type_New =
            CASE Type
                WHEN 'Deposit' THEN 1
                WHEN 'Withdraw' THEN 2
                WHEN 'Transfer' THEN 3
                ELSE 0
            END
    ");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Type_New",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Customers",
                type: "int",
                maxLength: 6,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAccounts");

            migrationBuilder.AddColumn<string>(
       name: "Type_Old",
       table: "Transactions",
       type: "nvarchar(max)",
       nullable: false,
       defaultValue: "");

            migrationBuilder.Sql(@"
        UPDATE Transactions
        SET Type_Old =
            CASE Type
                WHEN 1 THEN 'Deposit'
                WHEN 2 THEN 'Withdraw'
                WHEN 3 THEN 'Transfer'
                ELSE 'Unknown'
            END
    ");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Type_Old",
                table: "Transactions",
                newName: "Type");
        }
    }
}
