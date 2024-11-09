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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                .IsRequired()
                .HasComment("Id of the order");

            builder.Property(o => o.UserId)
                .IsRequired()
                .HasComment("The id of the user who made the order");

            builder.HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_User");

            builder.Property(o => o.DateOnOrderCreation)
                .IsRequired()
                .HasComment("The date on which the order was made");

            builder.Property(o => o.OrderDetailsId)
                .IsRequired()
                .HasComment("The id of the order details");

            builder.HasOne(o => o.OrderDetails)
                .WithMany()
                .HasForeignKey(o => o.OrderDetailsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_OrderDetails");

            builder.Property(o => o.StatusId)
                .IsRequired()
                .HasComment("The id of the order status");

            builder.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_Status");
        }
    }
}
