using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;

namespace Website.Data.Configurations
{
    using Models.Enums;
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            // Table Name
            builder.ToTable("ProductTypes");

            // Primary Key
            builder.HasKey(pt => pt.ProductTypeId);

            // Properties
            builder.Property(pt => pt.ProductTypeId)
                .IsRequired()
                .HasComment("Id of the gender type");

            builder.Property(pt => pt.ProductTypeName)
                .IsRequired()
                .HasConversion(
                    v => (int)v, // Store enum as int in the database
                    v => (ProductCategorizationEnumaration)v // Convert int back to enum
                )
                .HasComment("The gender declaration for a product");

            // Optional: Seed Data
            builder.HasData(
                new ProductType { ProductTypeId = 1, ProductTypeName = ProductCategorizationEnumaration.Male },
                new ProductType { ProductTypeId = 2, ProductTypeName = ProductCategorizationEnumaration.Female },
                new ProductType { ProductTypeId = 3, ProductTypeName = ProductCategorizationEnumaration.Kids },
                new ProductType { ProductTypeId = 4, ProductTypeName = ProductCategorizationEnumaration.Accessories }
            );
        }
    }
}
