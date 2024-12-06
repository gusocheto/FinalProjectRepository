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
    public class OrderUserConfiguration : IEntityTypeConfiguration<OrderUser>
    {
        public void Configure(EntityTypeBuilder<OrderUser> builder)
        {
            builder.ToTable("OrderUsers");

            builder.HasKey(ou => new { ou.ApplicationUserId, ou.OrderId });

            builder.Property(ou => ou.ApplicationUserId)
                   .IsRequired()
                   .HasComment("The ID of the user who made the order.");

            builder.Property(ou => ou.OrderId)
                   .IsRequired()
                   .HasComment("The ID of the user's order.");

            builder.HasOne(ou => ou.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(ou => ou.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ou => ou.Order)
                   .WithMany(o => o.OrderUsers)
                   .HasForeignKey(ou => ou.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
