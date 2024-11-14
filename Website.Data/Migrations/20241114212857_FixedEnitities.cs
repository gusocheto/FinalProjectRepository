using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedEnitities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "ProductTypes",
                newName: "ProductTypeId");

            migrationBuilder.RenameColumn(
                name: "IsAvaliable",
                table: "Products",
                newName: "IsAvailable");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "The type ID of the product",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The ID of the product",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The id of the product");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "CartsProducts",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The ID of the product in the cart",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartsProducts",
                type: "int",
                nullable: false,
                comment: "The ID of the cart to which this item belongs",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "ProductTypes",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Products",
                newName: "IsAvaliable");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The type ID of the product");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The id of the product",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The ID of the product");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "CartsProducts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The ID of the product in the cart");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartsProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The ID of the cart to which this item belongs");
        }
    }
}
