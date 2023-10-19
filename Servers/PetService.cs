using MongoDB.Bson;
using MongoDB.Driver;
using PawfectAppCore.Models;
using System.Runtime.CompilerServices;

namespace PawfectAppCore.Servers
{
    public class PetService : IPetService
    {
        private readonly IMongoCollection<Pet> _pets;
        public PetService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Pets");
            _pets = database.GetCollection<Pet>("PetSignupProfile");
        }

        public async Task<List<Pet>> GetPetsAsync()
        {
            return await _pets.Find(_ => true).ToListAsync();
        }

        public async Task<Pet> GetPetByPetIdAsync(string petId)
        {
            //var query_id = ObjectId.Parse(petId);
            var filter = Builders<Pet>.Filter.Eq("pid", ObjectId.Parse(petId));
            return await _pets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Pet>> GetPetsByUserId(string userId)
        {
            //var filter = Builders<Pet>.Filter.Eq("userID", userId);
            //return await _pets.Find(filter).ToListAsync();
            return await _pets.Find(d => d.UserId == userId).ToListAsync();
        }

        public async Task<List<Pet>> GetPetByTypeAndBreedAsync(string type, string breed)
        {
            var filter = Builders<Pet>.Filter.Eq("type", type) &
                (
                    Builders<Pet>.Filter.Eq("breeds.primary", breed) |
                    Builders<Pet>.Filter.Eq("breeds.secondary", breed) |
                    Builders<Pet>.Filter.Eq("breeds.mixed", breed) |
                    Builders<Pet>.Filter.Eq("breeds.unknown", breed) 
                );
            var matchingPets = await _pets.Find(filter).ToListAsync();
            return matchingPets;
        }

        public async Task<List<Pet>> GetPetsByTypeAsync(string type)
        {
            var filter = Builders<Pet>.Filter.Eq("type", type);
            return await _pets.Find(filter).ToListAsync();
        }

        public async Task<List<Pet>> GetPetsByBreedAsync(string breed)
        {
            var filter = Builders<Pet>.Filter.Or(
                Builders<Pet>.Filter.Eq("breeds.primary", breed),
                Builders<Pet>.Filter.Eq("breeds.secondary", breed),
                Builders<Pet>.Filter.Eq("breeds.mixed", breed),
                Builders<Pet>.Filter.Eq("breeds.unknown", breed)
                );
            return await _pets.Find(filter).ToListAsync();
        }

        public async Task<Pet> AddPetAsync(string userId, Pet pet)
        {
            pet.PetId = ObjectId.GenerateNewId().ToString();
            pet.UserId= userId;
            await _pets.InsertOneAsync(pet);
            return pet;
        }

        public async Task<Pet> UpdatePetAsync(string petId, Pet updatedPet)
        {
            var filterPet = Builders<Pet>.Filter.Eq("pid", ObjectId.Parse(petId));
            /*
            var update = Builders<Pet>.Update.Combine(
                updatedPet.GetType().GetProperties()
                .Select(prop =>
                    {
                        var value = prop.GetValue(updatedPet);
                        return Builders<Pet>.Update.Set(prop.Name, value);
                    })
            );
            */
  
            var update = Builders<Pet>.Update
                        .Set("profiles", updatedPet.Profiles)
                        .Set("videos", updatedPet.Videos)
                        .Set("name", updatedPet.Name)
                        .Set("birthDate", updatedPet.BirthDate)
                        .Set("type", updatedPet.Type)
                        .Set("breeds", updatedPet.Breeds)
                        .Set("colors", updatedPet.Colors)
                        .Set("size", updatedPet.Size)
                        .Set("about", updatedPet.About)
                        .Set("goodWith", updatedPet.GoodWith)
                        .Set("energyLevel", updatedPet.EnergyLevel)
                        .Set("coatLength", updatedPet.CoatLength)
                        .Set("careAndBehavior", updatedPet.CareAndBehavior)
                        .Set("gender", updatedPet.Gender)
                        .Set("published_at", updatedPet.Published)
                        .Set("contact", updatedPet.Contact)
                        .Set("organization_id", updatedPet.OrorganizationId);
            var pet = await _pets.FindOneAndUpdateAsync(filterPet, update, new FindOneAndUpdateOptions<Pet>
            {
                ReturnDocument = ReturnDocument.After
            });
            return pet;
        }

        public async Task DeletePetAsync(string petId)
        {
            var filter = Builders<Pet>.Filter.Eq("pid", ObjectId.Parse(petId));
            await _pets.DeleteOneAsync(filter);
        }

    }
}
