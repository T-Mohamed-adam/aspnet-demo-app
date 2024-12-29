namespace EmployeeAdminPortal.Models.Dtos
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public decimal Salary { get; set; }

        public Guid? DepartmentID { get; set; }

        public bool IsActive { get; set; } = true;

        // For image file
        public IFormFile? Image { get; set; }

    }
}
