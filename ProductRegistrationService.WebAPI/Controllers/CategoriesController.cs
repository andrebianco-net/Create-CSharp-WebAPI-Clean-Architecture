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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<TokenController> _logger;

        public CategoriesController(ICategoryService categoryService,
                                    ILogger<TokenController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            dynamic _return;

            try
            {

                var categories = await _categoryService.GetCategories();

                if (categories == null)
                {
                    _return = NotFound("Categories not found.");
                }

                _return = Ok(categories);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriesController.Get -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Get all Categories attempt.");
                _return = StatusCode(500, "Internal Server Error");
            }
        
            return _return;
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            dynamic _return;

            try
            {
                
                var category = await _categoryService.GetById(id);

                if (category == null)
                {
                    _return = NotFound("Category not found.");
                }

                _return = Ok(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriesController.GetCategory -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Get Category attempt.");
                _return = StatusCode(500, "Internal Server Error");                
            }
        
            return _return;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            dynamic _return;
 
            try
            {
                
                if(categoryDTO == null)
                {
                    _return = BadRequest("Invalid data.");
                }

                CategoryDTO newCategory = await _categoryService.Add(categoryDTO);

                _return = new CreatedAtRouteResult("GetCategory", new { id = newCategory.Id }, newCategory);

            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriesController.Post -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Post Category attempt.");
                _return = StatusCode(500, "Internal Server Error");                                
            }

            return _return;
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            dynamic _return;

            try
            {
                
                if(id != categoryDTO.Id)
                {
                    _return = BadRequest();
                }

                if(categoryDTO == null)
                {
                    _return = BadRequest();
                }

                await _categoryService.Update(categoryDTO);

                _return = Ok(categoryDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriesController.Put -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Put Category attempt.");
                _return = StatusCode(500, "Internal Server Error");
            }

            return _return;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            dynamic _return;

            try
            {

                var category = await _categoryService.GetById(id);

                if(category == null)
                {
                    _return = NotFound("Category not found.");
                }

                await _categoryService.Remove(id);

                _return = Ok(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriesController.Delete -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Delete Category attempt.");
                _return = StatusCode(500, "Internal Server Error");                
            }

            return _return;
        }

    }
}