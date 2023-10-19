using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public interface IBreederUserService
    {
        Task<List<Breeder>> GetBreedersAsync();
        Task<Breeder> GetBreederByIdAsync(string id);
        Task<Breeder> CreateBreederAsync (Breeder breeder);
        Task<Breeder> UpdateBreederAsync (string id, Breeder breeder);
        Task DeleteBreederAsync (string id);   
    }
}
