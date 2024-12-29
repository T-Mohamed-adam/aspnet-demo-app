using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Helpers;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;
        private readonly MembershipNumberHelper _membershipNumberHelper;

        public CategoryService(ApplicationDbContext dbContext, FileHelper fileHelper, MembershipNumberHelper membershipNumberHelper)
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
            _membershipNumberHelper = membershipNumberHelper;
        }

        // Get all catgories list
        public async Task<List<Category>> GetAllCategories()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            List<Category> categories = await _dbContext.Categories
                .Where(c => c.MembershipNumber == membershipNumber)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        // Get specific category by its id
        public async Task<Category?> GetCatgeoryById(Guid id)
        {
            Category? category = await _dbContext.Categories.FindAsync(id);

            if (category is null) 
            {
                return null;
            }

            return category;
        }

        // Add new category
        public async Task<Category?> AddCategory(AddCategoryDto addCategoryDto)
        {
            Category category = new Category()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description,
                MembershipNumber = _membershipNumberHelper.GetMembershipNumber(),
              
            };


            if (addCategoryDto.Image != null)
            {
                var imagePath = await _fileHelper.SaveImageAsync(addCategoryDto.Image);
                category.ImagePath = imagePath;
            }

            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
       
        // Update specific category by its id
        public async Task<Category?> UpdateCategory(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            Category? category = await _dbContext.Categories.FindAsync(id);

            if (category is null)
            {
                return null;
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;
            category.IsActive = updateCategoryDto.IsActive;

            if (updateCategoryDto.Image != null)
            {
                var imagePath = await _fileHelper.SaveImageAsync(updateCategoryDto.Image);
                category.ImagePath = imagePath;
            }

            await _dbContext.SaveChangesAsync();
            return category;

        }

        // Delete specific catgory by its id
        public async Task<Category?> DeleteCategory(Guid id)
        {
            Category? category = await _dbContext.Categories.FindAsync(id);

            if (category is null)
            {
                return null;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

       
    }
}
