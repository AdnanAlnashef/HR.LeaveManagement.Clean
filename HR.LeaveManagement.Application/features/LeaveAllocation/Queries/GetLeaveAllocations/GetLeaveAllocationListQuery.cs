using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>;
}
