using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class CreditssUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Credits");

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Credits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Credits");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Credits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
