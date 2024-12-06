using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models.Enums;
using Website.Data.Models;

namespace Website.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Statuses");

            builder.HasKey(s => s.StatusId);

            builder.Property(s => s.StatusId)
                .IsRequired()
                .HasComment("Id of the status type");

            builder.Property(s => s.StatusType)
                .IsRequired()
                .HasConversion(
                    v => (int)v,
                    v => (StatusEnumaration)v
                )
                .HasComment("Status type of the order (as an enum)");

            builder.HasData(
                new Status { StatusId = 1, StatusType = StatusEnumaration.Pending },
                new Status { StatusId = 2, StatusType = StatusEnumaration.Processing },
                new Status { StatusId = 3, StatusType = StatusEnumaration.Shipped },
                new Status { StatusId = 4, StatusType = StatusEnumaration.Delivered },
                new Status { StatusId = 5, StatusType = StatusEnumaration.Cancelled },
                new Status { StatusId = 6, StatusType = StatusEnumaration.Returned }
            );
        }
    }

}
