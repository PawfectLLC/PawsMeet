using Microsoft.AspNetCore.Mvc;
using PawfectAppCore.Models;
using PawfectAppCore.Servers;
using SharpCompress.Readers;

namespace PawfectAppCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreederController : Controller
    {
        private readonly IBreederUserService _breederUserService;
        public BreederController(IBreederUserService breederUserService)
        {
            _breederUserService = breederUserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Breeder>>> GetAllBreeders()
        {
            var breeders = await _breederUserService.GetBreedersAsync();
            return Ok(breeders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreederById(string id)
        {
            var breeder = await _breederUserService.GetBreederByIdAsync(id);
            return Ok(breeder);
        }

        [HttpPost]
        public async Task<ActionResult<Breeder>> CreateBreeder([FromBody] Breeder breeder)
        {
            var newbreeder = await _breederUserService.CreateBreederAsync(breeder);
            return Ok(newbreeder);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBreeder(string id)
        {
            await _breederUserService.DeleteBreederAsync(id);
            return Ok("Remove user");
        }

        [HttpPut]
        public async Task<ActionResult<Breeder>> UpdateBreeder(string id, [FromBody]Breeder breeder)
        {
            var updatebreeder = await _breederUserService.UpdateBreederAsync(id, breeder);
            return Ok(updatebreeder);
        }
    
    }
}
