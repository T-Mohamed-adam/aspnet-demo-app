namespace EmployeeAdminPortal.Models.Dtos
{
    public class UpdateOwnerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BusinessName { get; set; }

        public bool IsActive { get; set; }
    }
}
