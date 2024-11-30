using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Data.Models;
using Website.Data.Models.Enums;

namespace Website.Data.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            // Table name
            builder.ToTable("ProductTypes");

            // Primary key
            builder.HasKey(pt => pt.ProductTypeId);

            // Column configurations
            builder.Property(pt => pt.ProductTypeId)
                   .ValueGeneratedOnAdd() // Auto-increment if needed
                   .IsRequired()
                   .HasComment("Id of the gender type");

            builder.Property(pt => pt.ProductTypeName)
                   .HasConversion(
                       v => (int)v, // Enum to int for storage
                       v => (ProductCategorizationEnumaration)v // int back to enum
                   )
                   .IsRequired()
                   .HasComment("The gender declaration for a product");

            builder.HasData(
          new ProductType { ProductTypeId = 1, ProductTypeName = ProductCategorizationEnumaration.Male },
          new ProductType { ProductTypeId = 2, ProductTypeName = ProductCategorizationEnumaration.Female },
          new ProductType { ProductTypeId = 3, ProductTypeName = ProductCategorizationEnumaration.Kids },
          new ProductType { ProductTypeId = 4, ProductTypeName = ProductCategorizationEnumaration.Accessories }
          );
        }
    }
}
