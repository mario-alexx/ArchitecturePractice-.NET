using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Dotnet.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "Balance", "Name" },
                values: new object[] { 1, 0m, "Test Buyer 1" });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "UnitPrice", "Units" },
                values: new object[] { 1, null, 1, "Test Product 1", 10m, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BuyerId", "OrderStatus" },
                values: new object[] { 1, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
