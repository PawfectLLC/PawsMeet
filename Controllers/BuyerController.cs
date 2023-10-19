using Microsoft.AspNetCore.Mvc;
using PawfectAppCore.Models;
using PawfectAppCore.Servers;

namespace PawfectAppCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly IBuyerService _buyerService;
        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Buyer>>> GetAllBuyers()
        {
            var buyers = await _buyerService.GetBuyersAsync();
            if(buyers == null)
            {
                return NotFound();
            }
            return Ok(buyers);
        }

        [HttpGet("{buyerId}")]
        public async Task<ActionResult<Buyer>> GetBuyerById(string buyerId)
        {
            var buyer = await _buyerService.GetBuyerByIdAsync(buyerId);
            if(buyer == null)
            {
                return NotFound();
            }
            return Ok(buyer);
        }

        [HttpPost]
        public async Task<ActionResult<Buyer>> AddBuyer(string buyerId, [FromBody] Buyer buyer)
        {
            if(string.IsNullOrEmpty(buyerId))
            {
                return BadRequest("Unvalid buyer");
            }
            buyer.BuyerId = buyerId;
            await _buyerService.AddBuyerAsync(buyer);
            return Ok(buyer);
        }

        [HttpPut]
        public async Task<ActionResult<Buyer>> UpdateBuyer(string buyerId, [FromBody] Buyer buyer)
        {
            var newBuyer = await _buyerService.UpdateBuyerAsync(buyerId, buyer);
            return Ok(newBuyer);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBuyer(string buyerId)
        {
            await _buyerService.DeleteBuyerAsync(buyerId);
            return Ok("Remove buyer");
        }
    }
}
