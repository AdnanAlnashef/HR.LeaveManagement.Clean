using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequests;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<Domain.LeaveRequest, LeaveRequestListDto>().ReverseMap();
            CreateMap<Domain.LeaveRequest, LeaveRequestDetailsDto>().ReverseMap();
            CreateMap<Domain.LeaveRequest, UpdateLeaveRequestCommand>().ReverseMap();
        }
    }
}
