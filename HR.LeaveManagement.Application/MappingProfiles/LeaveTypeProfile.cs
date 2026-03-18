using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.features.LeaveType.Queries.GetAllLeaveType;
using HR.LeaveManagement.Application.features.LeaveType.Queries.GetAllLeaveTypeDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeCommand>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveTypeCommand>().ReverseMap();
        }
    }
}
