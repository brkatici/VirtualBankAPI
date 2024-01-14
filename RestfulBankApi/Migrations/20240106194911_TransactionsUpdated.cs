using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceAccountId",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "TargetAccountId",
                table: "AccountTransactions",
                newName: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountTransactions",
                newName: "TargetAccountId");

            migrationBuilder.AddColumn<int>(
                name: "SourceAccountId",
                table: "AccountTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
