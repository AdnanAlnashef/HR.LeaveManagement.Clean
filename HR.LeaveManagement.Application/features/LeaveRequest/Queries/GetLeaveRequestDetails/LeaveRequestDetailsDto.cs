using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.features.LeaveType.Queries.GetAllLeaveType;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsDto
    {
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequested { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public DateTime? DateActioned { get; set; }
        public string RequestComments { get; set; }
    }
}
