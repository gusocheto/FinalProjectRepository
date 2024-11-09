using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;

namespace Website.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(Website.Common.EntityValidationConstants.Product.ProductNameMaxLength)
                .HasComment("The name of the product");

            builder.Property(p => p.ProductPrice)
                .IsRequired()
                .HasComment("The price of the product");

            builder.Property(p => p.ProductDescription)
                .HasMaxLength(Website.Common.EntityValidationConstants.Product.ProductDetailsMaxLength)
                .HasComment("The product details");

            builder.Property(p => p.ImageUrl)
                .HasComment("The URL of the product image");

            builder.Property(p => p.StockQuantity)
                .IsRequired()
                .HasComment("The quantity of the product");

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Product_ProductType");

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Product_Category");

            builder.Property(p => p.IsAvaliable)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasComment("The true/false statement for the product availability");

            //builder.HasData(this.GenerateProducs());
        }

        private IEnumerable<Product> GenerateProducs()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product()
                {
                    ProductName = "Chair",
                    ProductPrice = 30,
                    ProductDescription = "Lorem ipsum is the best",
                    StockQuantity = 1,
                    CategoryTypeId = 1,
                    ProductTypeId = 1,
                    IsAvaliable = true,
                },
                new Product()
                {
                    ProductName = "Wall",
                    ProductPrice = 50,
                    ProductDescription = "Lorem ipsum is the best",
                    StockQuantity = 1,
                    CategoryTypeId = 1,
                    ProductTypeId = 1,
                    IsAvaliable = true,
                },
            };

            return products;
        }
    }
}
