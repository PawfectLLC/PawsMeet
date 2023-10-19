using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public interface IBuyerService
    {
        public Task<List<Buyer>> GetBuyersAsync();
        public Task<Buyer> GetBuyerByIdAsync(string buyerId);
        public Task<Buyer> AddBuyerAsync(Buyer buyer);
        public Task<Buyer> UpdateBuyerAsync(string buyerId, Buyer buyer);
        public Task DeleteBuyerAsync(string buyerId);
    }
}
