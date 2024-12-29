using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmployeeService _employeeService;

        // Create constructor and inject ApplicationDbContext in it
        public EmployeesController(ApplicationDbContext dbContext, IEmployeeService employeeService)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
        }

        // Get all employess
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await _employeeService.GetAllEmployees();

            return Ok(allEmployees);
        }

        // Fetch employee by its id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee =await _employeeService.GetEmployeeById(id);

            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        // Add new employee
        [HttpPost]
        public async Task<IActionResult?> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            try
            {
               Employee? employee = await _employeeService.AddEmployee(addEmployeeDto);

                return Ok(employee);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // Update specific employeee data

        [HttpPut("{id:guid}")]
        public async Task<IActionResult?> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto) 
        {
            try {
               Employee? employee = await _employeeService.UpdateEmployee(id, updateEmployeeDto);

                if (employee is null) 
                {
                    return NotFound("Employee not found");
                }

                return Ok(employee);

            } catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }

        // Delete specific employee data
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult?> DeleteEmployee(Guid id) 
        {
            Employee? employee = await _employeeService.DeleteEmployee(id); 

            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            return Ok("Employee deleted successfully");
        }

    }
}
