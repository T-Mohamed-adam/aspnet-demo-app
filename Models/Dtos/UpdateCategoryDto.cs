namespace EmployeeAdminPortal.Models.Dtos
{
    public class UpdateCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsActive { get; set; }
    }
}
