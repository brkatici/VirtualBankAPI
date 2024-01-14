using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class CreditsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestOfAmount",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "LoanAmount",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "LoanTermMonths",
                table: "Credits",
                newName: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "Credits",
                newName: "LoanTermMonths");

            migrationBuilder.AddColumn<decimal>(
                name: "InterestOfAmount",
                table: "Credits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LoanAmount",
                table: "Credits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
