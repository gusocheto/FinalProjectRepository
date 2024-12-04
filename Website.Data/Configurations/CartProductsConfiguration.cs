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
    public class CartProductsConfiguration : IEntityTypeConfiguration<CartProducts>
    {
        public void Configure(EntityTypeBuilder<CartProducts> builder)
        {
            builder.HasKey(cp => new { cp.ApplicationUserId, cp.ProductId });

            builder.HasOne(cp => cp.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(cp => cp.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Product)
                   .WithMany(p => p.CartProducts)
                   .HasForeignKey(cp => cp.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cp => cp.ApplicationUserId)
                   .IsRequired();

            builder.Property(cp => cp.ProductId)
                   .IsRequired();
        }
    }
}
