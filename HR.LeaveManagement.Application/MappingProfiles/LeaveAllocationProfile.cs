using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.features.LeaveAllocation.Queries.GetLeaveAllocations;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<Domain.LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<Domain.LeaveAllocation, LeaveAllocationDetailsDto>().ReverseMap();
            CreateMap<Domain.LeaveAllocation, CreateLeaveAllocationCommand>().ReverseMap();
        }
    }
}
