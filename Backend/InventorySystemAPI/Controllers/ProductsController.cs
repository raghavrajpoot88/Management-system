using InventorySystemAPI.CustomActionFilters;
using InventorySystemAPI.DTOs;
using InventorySystemAPI.Models;
using InventorySystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // POST: api/Products
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            try
            {
                var product = new Product
                {
                    ProductName = productDto.ProductName,
                    ProductDescription = productDto.ProductDescription,
                    FkProductCategory = productDto.FkProductCategory,
                    SellPrice = productDto.SellPrice,
                    CostPrice = productDto.CostPrice
                };

                var newProduct = await _productRepository.CreateAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductCreateDto productDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.ProductName = productDto.ProductName;
            existingProduct.ProductDescription = productDto.ProductDescription;
            existingProduct.FkProductCategory = productDto.FkProductCategory;
            existingProduct.SellPrice = productDto.SellPrice;
            existingProduct.CostPrice = productDto.CostPrice;

            await _productRepository.UpdateAsync(existingProduct);
            return Ok(existingProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            await _productRepository.DeleteAsync(product);
            return Ok(new { message = "Product deleted successfully.", product });
        }
    }
}
