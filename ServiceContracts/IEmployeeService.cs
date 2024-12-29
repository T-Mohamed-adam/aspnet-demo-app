using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.ServiceContracts
{
    public interface IEmployeeService
    {
        // Get all employees with it's department
        public Task<List<Employee>> GetAllEmployees();

        // Get specific employee by it's Id with it's department
        public Task<Employee?> GetEmployeeById(Guid id);

        // Add new employee
        public Task<Employee?> AddEmployee(AddEmployeeDto addEmployeeDto);


        // Update specific employee by it's Id
        public Task<Employee?> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto);

        // Delete specific employee by it's Id

        public Task<Employee?> DeleteEmployee(Guid id);
    }
}
