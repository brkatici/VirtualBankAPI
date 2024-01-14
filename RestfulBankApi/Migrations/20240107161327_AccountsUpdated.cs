using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class AccountsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbleToCredit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MinimumBalance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MobileWalletIntegration",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OnlineBanking",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AbleToCredit",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "InterestRate",
                table: "Accounts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumBalance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MobileWalletIntegration",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnlineBanking",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
