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
            builder.HasKey(cp => new { cp.CartId, cp.ProductId });

            builder.Property(cp => cp.CartId)
                .IsRequired();

            builder.Property(cp => cp.ProductId)
                .IsRequired();

            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CartProducts_Cart");

            builder.HasOne(cp => cp.Product)
                .WithMany()
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CartProducts_Product");
        }
    }
}
