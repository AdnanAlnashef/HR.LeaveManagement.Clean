using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Constracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.Management.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.Management.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsLeaveTypeUniqe(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => q.Name == name);
        }
    }
}
