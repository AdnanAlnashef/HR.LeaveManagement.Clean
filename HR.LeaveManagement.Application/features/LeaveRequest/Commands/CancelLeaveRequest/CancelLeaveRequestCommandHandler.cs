using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Constracts.Email;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            IEmailSender emailSender, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            if (leaveRequest.Approved == true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocation
                    (leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDay += daysRequested;
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
