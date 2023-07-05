using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("getBySubcategoryId/{subcategoryId}")]
        public async Task<IActionResult> GetBySubcategoryId([FromRoute] int advertismentId)
        {
            return Ok(await advertismentService.GetBySubcategoryId(advertismentId));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAdvertismentDTO advertisment)
        {
            await advertismentService.Create(advertisment);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] CreateAdvertismentDTO advertisment)
        {
            await advertismentService.Edit(advertisment);
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
