using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class TransfersUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceIBAN",
                table: "TransferTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetIBAN",
                table: "TransferTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceIBAN",
                table: "TransferTransactions");

            migrationBuilder.DropColumn(
                name: "TargetIBAN",
                table: "TransferTransactions");
        }
    }
}
