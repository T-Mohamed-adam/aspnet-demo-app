using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.ServiceContracts
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category?> GetCatgeoryById(Guid id);
        public Task<Category?> AddCategory(AddCategoryDto addCategoryDto);
        public Task<Category?> UpdateCategory(Guid id, UpdateCategoryDto updateCategoryDto);
        public Task<Category?> DeleteCategory(Guid id);

    }
}
