using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class customerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2ce41b6e-b939-40c7-a013-0837bbd93ef9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ef50d9a6-7c25-4bc4-90ea-f591657df05e"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CustomerUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("4ce544ca-7f8b-4b25-95b2-1627f2784c12"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 },
                    { new Guid("f92f2db5-16db-4382-81f7-5b18557c6a68"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerUsers_AspNetUsers_UserId",
                table: "CustomerUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerUsers_AspNetUsers_UserId",
                table: "CustomerUsers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4ce544ca-7f8b-4b25-95b2-1627f2784c12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f92f2db5-16db-4382-81f7-5b18557c6a68"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerUsers");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryTypeId", "ImageUrl", "IsAvailable", "ProductDescription", "ProductName", "ProductPrice", "ProductTypeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("2ce41b6e-b939-40c7-a013-0837bbd93ef9"), 1, null, true, "Lorem ipsum is the best", "Chair", 30m, 1, 1 },
                    { new Guid("ef50d9a6-7c25-4bc4-90ea-f591657df05e"), 1, null, true, "Lorem ipsum is the best", "Wall", 50m, 1, 1 }
                });
        }
    }
}
