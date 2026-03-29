using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Constracts.Identity;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository, IUserService userService)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocatinValidator = new CreateLeaveAllocationValidator(_leaveTypeRepository);
            var validationResult = await leaveAllocatinValidator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid leave Allocation request");

            //Get LeaveType for allocations
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            //Get Employees
            var employees = await _userService.GetEmployees();

            //Period
            var period = DateTime.Now.Year;

            var allocations = new List<Domain.LeaveAllocation>();
            foreach (var  emp in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);
                if (allocationExists == false)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDay = leaveType.DefaultDays,
                        Period = period
                    });
                }
            }

            if (allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);
            }

            //var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            //await _leaveAllocationRepository.CreateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
