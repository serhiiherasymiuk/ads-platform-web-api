using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService subcategoryService;

        public SubcategoriesController(ISubcategoryService subcategoryService)
        {
            this.subcategoryService = subcategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await subcategoryService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await subcategoryService.GetById(id));
        }
        [HttpGet("getByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int categoryId)
        {
            return Ok(await subcategoryService.GetByCategoryId(categoryId));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSubcategoryDTO subcategory)
        {
            await subcategoryService.Create(subcategory);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] CreateSubcategoryDTO subcategory)
        {
            await subcategoryService.Edit(subcategory);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await subcategoryService.Delete(id);
            return Ok();
        }
    }
}
