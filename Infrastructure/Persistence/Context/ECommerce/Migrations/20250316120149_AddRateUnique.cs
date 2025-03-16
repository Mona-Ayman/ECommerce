using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Context.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddRateUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRates_UserId",
                table: "ProductRates");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_UserId_ProductId",
                table: "ProductRates",
                columns: new[] { "UserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRates_UserId_ProductId",
                table: "ProductRates");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_UserId",
                table: "ProductRates",
                column: "UserId");
        }
    }
}
