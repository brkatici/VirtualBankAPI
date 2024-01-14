using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulBankApi.Migrations
{
    /// <inheritdoc />
    public partial class CreditsAddedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SupportMessages");

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestOfAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanTermMonths = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SupportMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
