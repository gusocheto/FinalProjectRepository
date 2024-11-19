using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryType",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "The name of the categoryType (as an enum)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The name of the categoryType");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryType" },
                values: new object[,]
                {
                    { 1, 10 },
                    { 2, 20 },
                    { 3, 30 },
                    { 4, 40 },
                    { 5, 50 },
                    { 6, 60 },
                    { 7, 70 },
                    { 8, 80 },
                    { 9, 90 },
                    { 10, 100 },
                    { 11, 110 },
                    { 12, 120 },
                    { 13, 130 },
                    { 14, 140 },
                    { 15, 150 },
                    { 16, 160 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "ProductTypeName" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("41a73c35-1681-45c9-8113-1136541c9224"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("a77beb9f-2d13-4183-b467-e02ce5468d3b"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("41a73c35-1681-45c9-8113-1136541c9224"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a77beb9f-2d13-4183-b467-e02ce5468d3b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryType",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "The name of the categoryType",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The name of the categoryType (as an enum)");
        }
    }
}
