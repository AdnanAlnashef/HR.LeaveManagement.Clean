using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.features.LeaveType.Queries.GetAllLeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDto? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
