using InventorySystemAPI.CustomActionFilters;
using InventorySystemAPI.DTOs;
using InventorySystemAPI.Models;
using InventorySystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSuppliersController : ControllerBase
    {
        public readonly IProductSupplierRepository _productSupplierRepository;

        public ProductSuppliersController(IProductSupplierRepository productSupplierRepository)
        {
            _productSupplierRepository = productSupplierRepository;
        }

        

        // POST: api/ProductSuppliers
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostProductSupplier([FromBody] ProductSupplierCreateDto productSupplierDto)
        {
            try
            {
                var productSupplier = new ProductSupplier
                {
                    FkProductId = productSupplierDto.FkProductId,
                    FkSupplierId = productSupplierDto.FkSupplierId
                };

                var newProductSupplier = await _productSupplierRepository.CreateAsync(productSupplier);
                return CreatedAtAction(nameof(GetProductSupplier), new { id = newProductSupplier.Id }, newProductSupplier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
