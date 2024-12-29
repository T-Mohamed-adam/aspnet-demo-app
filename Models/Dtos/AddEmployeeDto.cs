namespace EmployeeAdminPortal.Models.Dtos
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public decimal Salary { get; set; }

        public Guid? DepartmentID { get; set; }

        // For image file
        public IFormFile? Image { get; set; }
    }
}
