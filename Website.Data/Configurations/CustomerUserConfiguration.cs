using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;
using static Website.Common.EntityValidationConstants.CustomerUser;

namespace Website.Data.Configurations
{
    public class CustomerUserConfiguration : IEntityTypeConfiguration<CustomerUser>
    {
        public void Configure(EntityTypeBuilder<CustomerUser> builder)
        {
            builder.HasKey(cu => cu.CustomerId);

            builder.Property(cu => cu.CustomerId)
                .HasComment("The id of the customer");

            builder.Property(cu => cu.Username)
                .IsRequired()
                .HasMaxLength(CustomerNameMaxLength)
                .HasComment("Username of the customer");

            builder.Property(cu => cu.Password)
                .IsRequired()
                .HasComment("The password of the customer");

            builder.Property(cu => cu.Email)
                .IsRequired()
                .HasComment("The email of the user");

            builder.Property(cu => cu.Address)
                .IsRequired()
                .HasComment("The address of the customer");

            builder.Property(cu => cu.CartId)
                .IsRequired()
                .HasComment("The id of the user's cart");

            builder.HasOne(cu => cu.Cart)
                .WithOne()
                .HasForeignKey<CustomerUser>(cu => cu.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CustomerUser_Cart");

            builder.HasMany(cu => cu.Orders)
                .WithOne()
                .HasForeignKey("CustomerUserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
