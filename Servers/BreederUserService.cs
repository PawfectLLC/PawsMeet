using MongoDB.Driver;
using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public class BreederUserService : IBreederUserService
    {
        private readonly IMongoCollection<Breeder> _breeders;
        public BreederUserService(IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase("BaseUsers");
            _breeders = database.GetCollection<Breeder>("BreederSignUpProfile");
        }
        public async Task<Breeder> CreateBreederAsync(Breeder breeder)
        {
            await _breeders.InsertOneAsync(breeder);
            return breeder;
        }

        public async Task DeleteBreederAsync(string breederId)
        {
            var filter = Builders<Breeder>.Filter.Eq("breederId", breederId);
            await _breeders.DeleteOneAsync(filter);
        }

        public async Task<Breeder> GetBreederByIdAsync(string breederId)
        {
            var filter = Builders<Breeder>.Filter.Eq("breederId", breederId);
            return await _breeders.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<List<Breeder>> GetBreedersAsync()
        {          
            return await _breeders.Find(_ => true).ToListAsync();
        }

        public async Task<Breeder> UpdateBreederAsync(string breederId, Breeder breeder)
        {
            var filter = Builders<Breeder>.Filter.Eq("breederId", breederId);
            var update = Builders<Breeder>.Update
                             .Set("org_verification", breeder.orgVerification)
                             .Set("email", breeder.Email)
                             .Set("desc", breeder.Desc)
                             .Set("region", breeder.Region)
                             .Set("firstName", breeder.FirstName)
                             .Set("lastName", breeder.LastName);
            var newBreeder = _breeders.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Breeder>
            {
                ReturnDocument = ReturnDocument.After
            });
            return newBreeder;
        }
    }
}
