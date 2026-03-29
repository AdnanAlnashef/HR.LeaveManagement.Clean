using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Constracts.Email;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public ChangeLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper, IEmailSender emailSender, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            //if request is approved, get and update the employees allocations
            if (request.Approved)
            {
                var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocation
                    (leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDay -= daysRequested;
                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Leave Request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                        $"has been cancelled successfully",
                Subject = "Leave Request Cancelled"
            };
            await _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}
