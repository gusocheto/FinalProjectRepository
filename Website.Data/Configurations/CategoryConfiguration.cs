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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table Name
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.CategoryId);

            // Properties
            builder.Property(c => c.CategoryId)
                .IsRequired()
                .HasComment("The id of the categoryType");

            builder.Property(c => c.CategoryType)
                .IsRequired()
                .HasConversion(
                    v => (int)v, // Store enum as int in the database
                    v => (CategoryEnumaration)v // Convert int back to enum
                )
                .HasComment("The name of the categoryType (as an enum)");

            // Seed Data
            builder.HasData(
                new Category { CategoryId = 1, CategoryType = CategoryEnumaration.TShirts },
                new Category { CategoryId = 2, CategoryType = CategoryEnumaration.Shirts },
                new Category { CategoryId = 3, CategoryType = CategoryEnumaration.Jeans },
                new Category { CategoryId = 4, CategoryType = CategoryEnumaration.Jackets },
                new Category { CategoryId = 5, CategoryType = CategoryEnumaration.Suits },
                new Category { CategoryId = 6, CategoryType = CategoryEnumaration.Dresses },
                new Category { CategoryId = 7, CategoryType = CategoryEnumaration.TopsAndBlouses },
                new Category { CategoryId = 8, CategoryType = CategoryEnumaration.Skirts },
                new Category { CategoryId = 9, CategoryType = CategoryEnumaration.PantsAndShorts },
                new Category { CategoryId = 10, CategoryType = CategoryEnumaration.Shoes },
                new Category { CategoryId = 11, CategoryType = CategoryEnumaration.Bags },
                new Category { CategoryId = 12, CategoryType = CategoryEnumaration.Belts },
                new Category { CategoryId = 13, CategoryType = CategoryEnumaration.HatsAndCaps },
                new Category { CategoryId = 14, CategoryType = CategoryEnumaration.Sunglasses },
                new Category { CategoryId = 15, CategoryType = CategoryEnumaration.Jewelry },
                new Category { CategoryId = 16, CategoryType = CategoryEnumaration.Watches }
            );
        }
    }
}
