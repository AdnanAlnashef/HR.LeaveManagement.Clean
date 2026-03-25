using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c41a7d1d-e89f-4c85-b354-82b49f529405",
                    UserId = "c56ca925-a881-4588-9998-ae4132ac34e7"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "6e3c155f-079e-4942-ba27-c2271519b420",
                    UserId = "5773ca9d-dd22-456f-9181-0e4701122e34"
                }
            );
        }
    }
}
