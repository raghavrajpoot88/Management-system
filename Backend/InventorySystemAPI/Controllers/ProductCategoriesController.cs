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
    public class ProductCategoriesController : ControllerBase
    {
        public readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoriesController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        // POST: api/ProductCategories
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProductCategory([FromBody] ProductCategoryCreateDto productCategoryDto)
        {
            try
            {
                var productCategory = new ProductCategory
                {
                    ProductCategoryName = productCategoryDto.ProductCategoryName
                };

                var newProductCategory = await _productCategoryRepository.CreateAsync(productCategory);
                return CreatedAtAction(nameof(GetProductCategory), new { id = newProductCategory.Id }, newProductCategory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
