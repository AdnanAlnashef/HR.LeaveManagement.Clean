using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Constracts.Identity;
using HR.LeaveManagement.Application.Constracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Queries.GetLeaveRequests
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
            IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            //Check if it is logged in employee
            if (request.IsLoggedInUser)
            {
                var userId = _userService.UserId;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

                //Fill requests with employee information

                return requests;
        }
    }
}
