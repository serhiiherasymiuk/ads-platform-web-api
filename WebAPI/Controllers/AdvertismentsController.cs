using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentsController : ControllerBase
    {
        private readonly IAdvertismentService advertismentService;

        public AdvertismentsController(IAdvertismentService advertismentService)
        {
            this.advertismentService = advertismentService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await advertismentService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await advertismentService.GetById(id));
        }
        [HttpGet("getByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] int categoryId)
        {
            return Ok(await advertismentService.GetByCategoryId(categoryId));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAdvertismentDTO advertisment)
        {
            await advertismentService.Create(advertisment);
            return Ok();
        }
        [HttpPut("{advertismentId}")]
        public async Task<IActionResult> Edit(int advertismentId, [FromForm] CreateAdvertismentDTO advertisment)
        {
            await advertismentService.Edit(advertismentId, advertisment);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await advertismentService.Delete(id);
            return Ok();
        }
    }
}
