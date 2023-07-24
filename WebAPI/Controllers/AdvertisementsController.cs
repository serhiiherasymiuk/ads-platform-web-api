using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService advertisementService;

        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await advertisementService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await advertisementService.GetById(id));
        }
        [HttpGet("getByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] int categoryId)
        {
            return Ok(await advertisementService.GetByCategoryId(categoryId));
        }
        [HttpGet("getByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetByCategoryName([FromRoute] string categoryName)
        {
            return Ok(await advertisementService.GetByCategoryName(categoryName));
        }
        [HttpGet("Search/{query}")]
        public async Task<IActionResult> Search([FromRoute] string query)
        {
            return Ok(await advertisementService.Search(query));
        }
        [HttpGet("SearchByCategory/{query}/{categoryName}")]
        public async Task<IActionResult> SearchByCategory([FromRoute] string query, string categoryName)
        {
            return Ok(await advertisementService.SearchByCategory(query, categoryName));
        }
        [HttpGet("SearchByCategory/{categoryName}")]
        public async Task<IActionResult> SearchByCategory([FromRoute] string categoryName)
        {
            return Ok(await advertisementService.SearchByCategory(null, categoryName));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAdvertisementDTO advertisement)
        {
            await advertisementService.Create(advertisement);
            return Ok();
        }
        [HttpPut("{advertisementId}")]
        public async Task<IActionResult> Edit(int advertisementId, [FromForm] CreateAdvertisementDTO advertisement)
        {
            await advertisementService.Edit(advertisementId, advertisement);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await advertisementService.Delete(id);
            return Ok();
        }
    }
}
