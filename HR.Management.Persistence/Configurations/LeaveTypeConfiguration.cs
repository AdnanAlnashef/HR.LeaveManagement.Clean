using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Management.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
               new LeaveType
               {
                   Id = 1,
                   Name = "Vacation",
                   DefaultDays = 10,
                   CreatedDate = DateTime.Now,
                   ModifiedDate = DateTime.Now
               }
           );

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
