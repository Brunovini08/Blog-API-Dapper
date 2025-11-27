using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet()]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
           try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                if(categories is null)
                    return NotFound("Nenhuma categoria registrada");
                return Ok(categories);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
           try
            {
                var category = await _categoryService.GetByIdCategoryAsync(id);
                if (category is null)
                    return NotFound("Categoria não encontrada");
                return Ok(category);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryRequestDTO category)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(category);
                return Created();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(CategoryRequestDTO category, int id)
        {
           try
            {
                await _categoryService.UpdateCategoryAsync(category, id);
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
    }
}
