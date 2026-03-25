using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
