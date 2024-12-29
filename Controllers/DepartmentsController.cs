using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all departments
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _dbContext.Departments.ToList();

            return Ok(departments);
        }

        // Get department by it's id
        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetDepartmentByID(Guid id)
        {
            var department = _dbContext.Departments.Find(id);

            if (department is null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);

        }


        // Add new department
        [HttpPost]
        public IActionResult AddDepartment(Guid id, [FromForm] AddDepartmentDto addDepartmentDto) 
        {
            Department departmentEntity = new Department()
            {
                Name = addDepartmentDto.Name,
                Location = addDepartmentDto.Location,
                Description = addDepartmentDto.Description,
            };

            _dbContext.Add(departmentEntity);
            _dbContext.SaveChanges();

            return Ok(departmentEntity);
        }

        // Update department by it's id
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult AddDepartment(Guid id, [FromForm] UpdateDepartmentDto updateDepartmentDto) 
        {
            var department = _dbContext.Departments.Find(id);

            if (department is null)
            {
                return NotFound("Department not found");
            }

            department.Name = updateDepartmentDto.Name;
            department.Location = updateDepartmentDto.Location;
            department.Description = updateDepartmentDto.Description;
            _dbContext.SaveChanges();

            return Ok(department);
        }

        // Delete department by it's id
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteDepartment(Guid id) 
        {
            var department = _dbContext.Departments.Find(id);

            if (department is null)
            {
                return NotFound("Department not found");
            }

            _dbContext.Remove(department);
            _dbContext.SaveChanges();

            return Ok("Department deleted successfully");
        }
    }
}
