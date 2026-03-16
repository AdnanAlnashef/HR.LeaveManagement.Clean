using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Constracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUniqe(string name);
    }
}
