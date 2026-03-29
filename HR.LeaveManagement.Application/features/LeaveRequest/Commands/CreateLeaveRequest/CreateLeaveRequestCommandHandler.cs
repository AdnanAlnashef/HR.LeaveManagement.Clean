using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Constracts.Email;
using HR.LeaveManagement.Application.Constracts.Identity;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly ILeaveAllocationRepository _leaveALlocationRepository;

        public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
            ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender,
            IUserService userService, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _userService = userService;
            _leaveALlocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
            {
                throw new BadRequestException("Invalid leave request", validationResult);
            }

            //Get requesting employee's id
            var employeeId = _userService.UserId;

            //Check on employee's allocation
            var allocation = await _leaveALlocationRepository.GetUserAllocation(employeeId, request.LeaveTypeId);

            //if allocation arn't enough, return validation error with message
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(
                    request.LeaveTypeId), "You dont have any allocations for this Leave Type."));
                throw new BadRequestException("Invalid leave request", validationResult);
            }

            var davsRequested = (int)(request.EndDate - request.StartDate).TotalDays;
            if (davsRequested > allocation.NumberOfDay)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(
                        request.EndDate), "You dont have enough days for this request."));
                throw new BadRequestException("Invalid leave request", validationResult);
            }

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employeeId;
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Leave Request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                            $"has been updated successfully",
                Subject = "Leave Request Updated"
            };
            await _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}
