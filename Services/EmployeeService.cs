using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using EmployeeAdminPortal.Helpers;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;


        public EmployeeService(ApplicationDbContext dbContext, FileHelper fileHelper)
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
        }

        // Get all employess
        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> allEmployees = await _dbContext.Employees.Include(e => e.Department).ToListAsync();

            return allEmployees;
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            Employee? employee = await _dbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<Employee?> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
                DepartmentID = addEmployeeDto.DepartmentID,
            };

            if (addEmployeeDto.Image != null) 
            {
                var imagePath = await _fileHelper.SaveImageAsync(addEmployeeDto.Image);
                employee.ProfilePicturePath = imagePath;
            }

           await _dbContext.Employees.AddAsync(employee);
           await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee is null)
            {
                return null;
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.DepartmentID = updateEmployeeDto.DepartmentID;
            employee.IsActive = updateEmployeeDto.IsActive;

            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> DeleteEmployee(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee is null)
            {
                return null;
            }

            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync();
            return employee;
        }
    }
}
