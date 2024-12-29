using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/categories
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }

        // Gel all products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _productService.GetAllProducts();

            return Ok(products);
        }

        // Get specific product by its id
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult?> GetProductById(Guid id)
        {
            Product? product = await _productService.GetProductById(id);
            if (product is null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        // Add new product 
        [HttpPost]
        public async Task<IActionResult?> AddProuduct([FromForm] AddProductDto addProductDto)
        {
            try
            {
                Product? product = await _productService.AddProduct(addProductDto);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific product by its id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult?> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            try
            {
                Product? product = await _productService.UpdateProduct(id, updateProductDto);

                if (product is null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Delete specific product by its id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult?> DeleteProduct(Guid id)
        {
            Product? product = await _productService.DeleteProduct(id);
            if (product is null)
            {
                return NotFound("Product not found");
            }

            return Ok("Product deleted successfully");
        }
    }
}
