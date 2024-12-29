namespace EmployeeAdminPortal.Models.Dtos
{
    public class OwnerResponseDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BusinessName { get; set; }
        public string? MembershipNumber { get; set; }
    }
}
