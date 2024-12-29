namespace EmployeeAdminPortal.Models.Dtos
{
    public class AddDepartmentDto
    {
        public required string Name { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }
    }
}
