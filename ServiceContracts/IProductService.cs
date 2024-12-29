using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.ServiceContracts
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product?> GetProductById(Guid id);
        public Task<Product?> AddProduct(AddProductDto addProductDto);
        public Task<Product?> UpdateProduct(Guid id, UpdateProductDto updateProductDto);
        public Task<Product?> DeleteProduct(Guid id);
    }
}
