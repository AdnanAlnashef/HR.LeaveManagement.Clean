using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Constracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetAllocationsWithDetails();
        Task<List<LeaveAllocation>> GetAllocationsWithDatails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId);
    }
}
