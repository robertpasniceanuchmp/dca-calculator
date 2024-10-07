using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DcaCalculator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoinmarketCapId",
                table: "Cryptocurrencies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinmarketCapId",
                table: "Cryptocurrencies");
        }
    }
}
