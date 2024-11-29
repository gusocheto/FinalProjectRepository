using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4ce544ca-7f8b-4b25-95b2-1627f2784c12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f92f2db5-16db-4382-81f7-5b18557c6a68"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("ab96b478-2dc7-4433-8e76-70c42aeaefe0"), 10, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("bb51e33e-511f-4e8f-a335-03b9307d84e5"), 10, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ab96b478-2dc7-4433-8e76-70c42aeaefe0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("bb51e33e-511f-4e8f-a335-03b9307d84e5"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("4ce544ca-7f8b-4b25-95b2-1627f2784c12"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("f92f2db5-16db-4382-81f7-5b18557c6a68"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });
        }
    }
}
