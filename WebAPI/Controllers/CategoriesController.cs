using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await categoryService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await categoryService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO category)
        {
            await categoryService.Create(category);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int categoryId, [FromForm] CreateCategoryDTO category)
        {
            await categoryService.Edit(categoryId, category);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await categoryService.Delete(id);
            return Ok();
        }
    }
}
