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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.CartID);

            builder.Property(c => c.CartID)
                .IsRequired()
                .HasComment("The id of the cart");

            builder.HasMany(c => c.CartItems)
                .WithOne(cp => cp.Cart)
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Cart_CartProducts");
        }
    }
}
