using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.ServiceContracts;

namespace EmployeeAdminPortal.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _dbContext;


        public DepartmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
