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
        private readonly ILogger<TokenController> _logger;

        public ProductsController(IProductService ProductService,
                                  ILogger<TokenController> logger)
        {
            _ProductService = ProductService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            dynamic _return;        
            
            try
            {

                var Products = await _ProductService.GetProducts();

                if (Products == null)
                {
                    _return = NotFound("Products not found.");
                }

                _return = Ok(Products);
                
            }
            catch (Exception ex)
            {                
                _logger.LogError($"ProductsController.Get -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Get all Products attempt.");
                _return = StatusCode(500, "Internal Server Error");                
            }

            return _return;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            dynamic _return;
            
            try
            {

                var Product = await _ProductService.GetById(id);

                if (Product == null)
                {
                    _return = NotFound("Product not found.");
                }

                _return = Ok(Product);
                
            }
            catch (Exception ex)
            {                
                _logger.LogError($"ProductsController.GetProducts -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Get Product attempt.");
                _return = StatusCode(500, "Internal Server Error");                                
            }
            
            return _return;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO ProductDTO)
        {
            dynamic _return; 
            
            try
            {

                if(ProductDTO == null)
                {
                    _return = BadRequest("Invalid data.");
                }

                ProductDTO newProduct = await _ProductService.Add(ProductDTO);

                _return = new CreatedAtRouteResult("GetProduct", new { id = newProduct.Id }, newProduct);
                
            }
            catch (Exception ex)
            {                
                _logger.LogError($"ProductsController.Post -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Post Product attempt.");
                _return = StatusCode(500, "Internal Server Error");                                
            }
            
            return _return; 
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO ProductDTO)
        {
            dynamic _return;
            
            try
            {

                if(id != ProductDTO.Id)
                {
                    _return = BadRequest();
                }

                if(ProductDTO == null)
                {
                    _return = BadRequest();
                }

                await _ProductService.Update(ProductDTO);

                _return = Ok(ProductDTO);
                
            }
            catch (Exception ex)
            {                
                _logger.LogError($"ProductsController.Put -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Put Product attempt.");
                _return = StatusCode(500, "Internal Server Error");                                
            }
            
            return _return;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            dynamic _return;
            
            try
            {

                var Product = await _ProductService.GetById(id);

                if(Product == null)
                {
                    _return = NotFound("Product not found.");
                }

                await _ProductService.Remove(id);

                _return = Ok(Product);
                
            }
            catch (Exception ex)
            {                
                _logger.LogError($"ProductsController.Delete -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Delete Product attempt.");
                _return = StatusCode(500, "Internal Server Error");                                
            }
            
            return _return;
        }

    }
}