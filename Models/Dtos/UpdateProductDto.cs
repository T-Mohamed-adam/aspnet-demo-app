namespace EmployeeAdminPortal.Models.Dtos
{
    public class UpdateProductDto
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        // For image file
        public IFormFile? Image { get; set; }

        public decimal Price { get; set; }

        public int Cal { get; set; }

        public string? MembershipNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
