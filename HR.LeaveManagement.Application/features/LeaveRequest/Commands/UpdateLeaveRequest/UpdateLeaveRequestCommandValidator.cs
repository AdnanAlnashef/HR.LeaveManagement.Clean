using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Application.features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;

            Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

            RuleFor(q => q.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
        {
              var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
              return leaveRequest != null;
        }
    }
}
