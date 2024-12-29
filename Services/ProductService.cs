using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Helpers;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        public ProductService(ApplicationDbContext dbContext, FileHelper fileHelper, MembershipNumberHelper membershipNumberHelper) 
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
            _membershipNumberHelper = membershipNumberHelper;
        }

        // Get all products
        public async Task<List<Product>> GetAllProducts()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            List<Product> products = await _dbContext.Products.Include(p => p.Category)
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false && p.IsActive == true)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return products;
        }

        // Get specific product data 
        public async Task<Product?> GetProductById(Guid id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);

            if (product is null)
            {
                return null;
            }

            return product;
        }

        // Add new product
        public async Task<Product?> AddProduct(AddProductDto addProductDto)
        {
            Product product = new Product()
            {
                Name = addProductDto.Name,
                CategoryId = addProductDto.CategoryId,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                Cal = addProductDto.Cal,
                MembershipNumber = _membershipNumberHelper.GetMembershipNumber(),

            };

            if (addProductDto.Image != null)
            {
                var imagePath = await _fileHelper.SaveImageAsync(addProductDto.Image);
                product.ImagePath = imagePath;
            }

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }


        // Update specific product data 
        public async Task<Product?> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            Product? product = await _dbContext.Products.FindAsync(id);

            if (product is null)
            {
                return null;
            }

            product.Name = updateProductDto.Name;
            product.CategoryId = updateProductDto.CategoryId;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.Cal = updateProductDto.Cal;
            product.IsActive = updateProductDto.IsActive;

            if (updateProductDto.Image != null)
            {
                var imagePath = await _fileHelper.SaveImageAsync(updateProductDto.Image);
                product.ImagePath = imagePath;
            }

            await _dbContext.SaveChangesAsync();
            return product;
        }

        // Delete specific product data 
        public async Task<Product?> DeleteProduct(Guid id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);

            if (product is null)
            {
                return null; 
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
