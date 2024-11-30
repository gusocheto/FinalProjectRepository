using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ab96b478-2dc7-4433-8e76-70c42aeaefe0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("bb51e33e-511f-4e8f-a335-03b9307d84e5"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "CartsProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("a0440eee-107f-48f5-8d3e-6b4769c337a8"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("a23f8edd-5f91-4946-ae81-c1c4b364ce29"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartsProducts_ProductId1",
                table: "CartsProducts",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartsProducts_Products_ProductId1",
                table: "CartsProducts",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartsProducts_Products_ProductId1",
                table: "CartsProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartsProducts_ProductId1",
                table: "CartsProducts");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a0440eee-107f-48f5-8d3e-6b4769c337a8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a23f8edd-5f91-4946-ae81-c1c4b364ce29"));

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "CartsProducts");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("ab96b478-2dc7-4433-8e76-70c42aeaefe0"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("bb51e33e-511f-4e8f-a335-03b9307d84e5"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });
        }
    }
}
