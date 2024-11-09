using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Website.Common.EntityValidationConstants;
using Website.Data.Models;

namespace Website.Data.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(od => od.OrderDetailsID);

            builder.Property(od => od.OrderDetailsID)
                .IsRequired()
                .HasComment("The id of the order details");

            builder.Property(od => od.CustomerUserID)
                .IsRequired()
                .HasComment("The id of the current user");

            builder.HasOne(od => od.CustomerUser)
                .WithMany()
                .HasForeignKey(od => od.CustomerUserID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderDetails_CustomerUser");

            builder.Property(od => od.ShippingAddress)
                .IsRequired()
                .HasComment("The shipping address of the order");

            builder.Property(od => od.City)
                .IsRequired()
                .HasMaxLength(OderDetails.CityNameMaxLength)
                .HasComment("The city for the shipping address");

            builder.Property(od => od.Country)
                .IsRequired()
                .HasMaxLength(OderDetails.CountryNameMaxLength)
                .HasComment("The country for the shipping address");

            builder.Property(od => od.ZipCode)
                .IsRequired()
                .HasComment("The zip code of the city");

            builder.Property(od => od.AmountPaid)
                .IsRequired()
                .HasComment("The order price");
        }
    }
}
