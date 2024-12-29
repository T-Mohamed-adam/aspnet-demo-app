using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models.Dtos
{
    public class AddCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MembershipNumber { get; set; }
        public IFormFile? Image { get; set; }
    }
}
