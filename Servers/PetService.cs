using MongoDB.Bson;
using MongoDB.Driver;
using PawfectAppCore.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Python.Runtime;
using Nest;
using Elasticsearch.Net;

namespace PawfectAppCore.Servers
{
    public class PetService : IPetService
    {
        private readonly IMongoCollection<Pet> _pets;
        private int offset = 0;
        private const int PageSize = 20;
        public PetService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Pets");
            _pets = database.GetCollection<Pet>("PetSignupProfile");
        }

        public async Task<List<Pet>> GetPetsAsync()
        {
            var nextSet = await _pets.Find(_ => true)
                                     .Skip(offset)
                                     .Limit(PageSize)
                                     .ToListAsync(); 
            
            offset += PageSize;
            return nextSet;
        }

        //Get pet by pid (not _id)
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

        public List<string> PetSearchingModel(string userInput)
        {
            var petIds = new List<string>();
            var psi = new ProcessStartInfo
            {
                FileName = @"G:\Python\python.exe",
                Arguments = @"H:\PetCode\Paw\PawfectAppCore\recommender_matching_v1.py {userInput}"
            };
            
            //var script = @"H:\PetCode\Paw\PawfectAppCore\hello.py";
            //var name1 = "LYNN";
            //var name2 = "Li";
            //psi.Arguments = $"\"{script}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //var errors = "";
            //var results = "";
            using(var process = new Process() )
            {
                process.StartInfo = psi ;
                process.Start();
                var errorTask = process.StandardError.ReadToEnd();
                var outputTask = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                var output = outputTask.ToString();
                var error = errorTask.ToString();

                //Get the pets id from output and convert it to list
                var topFiveIndex = output.IndexOf("Top five pet IDs:");
                if (topFiveIndex != -1)
                {
                    var startIndex = topFiveIndex + "Top five pet IDs:".Length;
                    var petIdsString = output.Substring(startIndex).Trim();
                    petIds = petIdsString.Split(' ').ToList();

                    Console.WriteLine("Retrieved pet IDs from Python:", string.Join(", ", petIds));
                }

                Console.WriteLine("errors: " + output);
                Console.WriteLine("output: " + error);
            }
            return petIds;
        }

        public List<Pet> GetPetsNearMe(string petLocation, List<Pet> allpets)
        {
            List<string> neighborStates = StatesData.NeighboringStates.ContainsKey(petLocation)
                ? StatesData.NeighboringStates[petLocation] : new List<string>();

            List<Pet> nearPets = new List<Pet>();
            foreach(var pet in allpets)
            {
                string petState = pet.Contact.Address.State;
                if (petState == petLocation || neighborStates.Contains(petState)) {
                    nearPets.Add(pet);
                }
            }

            return nearPets;
        } 
    }
}
