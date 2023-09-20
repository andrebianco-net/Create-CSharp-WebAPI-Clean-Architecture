using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRegistrationService.Application.DTOs;
using ProductRegistrationService.Application.interfaces;

namespace ProductRegistrationService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
        
            var Products = await _ProductService.GetProducts();

            if (Products == null)
            {
                return NotFound("Products not found.");
            }

            return Ok(Products);

        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
        
            var Product = await _ProductService.GetById(id);

            if (Product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(Product);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO ProductDTO)
        {
 
            if(ProductDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            await _ProductService.Add(ProductDTO);

            return new CreatedAtRouteResult("GetProduct", new { id = ProductDTO.Id }, ProductDTO);
 
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO ProductDTO)
        {

            if(id != ProductDTO.Id)
            {
                return BadRequest();
            }

            if(ProductDTO == null)
            {
                return BadRequest();
            }

            await _ProductService.Update(ProductDTO);

            return Ok(ProductDTO);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {

            var Product = await _ProductService.GetById(id);

            if(Product == null)
            {
                return NotFound("Product not found.");
            }

            await _ProductService.Remove(id);

            return Ok(Product);

        }

    }
}