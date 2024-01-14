using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class AccountsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AbleToCredit = table.Column<bool>(type: "bit", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterestRate = table.Column<double>(type: "float", nullable: true),
                    MinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnlineBanking = table.Column<bool>(type: "bit", nullable: false),
                    DailyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DailyTransferLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AutomaticPayments = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MobileWalletIntegration = table.Column<bool>(type: "bit", nullable: false),
                    SpecialNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
