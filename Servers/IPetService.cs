using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public interface IPetService
    {
        public Task<List<Pet>> GetPetsAsync();
        public Task<Pet> GetPetByPetIdAsync(string petId);
        public Task<List<Pet>> GetPetByTypeAndBreedAsync(string type, string breed);
        public Task<List<Pet>> GetPetsByTypeAsync(string type);
        public Task<List<Pet>> GetPetsByBreedAsync(string breed);
        public Task<List<Pet>> GetPetsByUserId(string userId);
        public Task<Pet> AddPetAsync(string userId, Pet pet);
        public Task<Pet> UpdatePetAsync(string petId, Pet pet);
        public Task DeletePetAsync(string petId);
    }
}
