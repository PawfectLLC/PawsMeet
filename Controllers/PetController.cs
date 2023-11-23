using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PawfectAppCore.Models;
using PawfectAppCore.Servers;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace PawfectAppCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<List<Pet>> GetAllPets()
        {
            List<Pet> allPets = new List<Pet>();
            List<Pet> nextSet;

            do
            {
                nextSet = await _petService.GetPetsAsync();
                allPets.AddRange(nextSet);
            }
            while (nextSet.Count == 20);

            return allPets;
        }

        [HttpGet("{petId}")]
        public async Task<ActionResult<Pet>> GetPetByPetId(string petId)
        {
            var pet = await _petService.GetPetByPetIdAsync(petId);
            if(pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpGet("petsByUserId/{userId}", Name = "GetPetsByUserId")]
        public async Task<ActionResult<List<Pet>>> GetPetsByUserId(string userId)
        {
            var pets = await _petService.GetPetsByUserId(userId);
            if(pets == null)
            {
                return NotFound();
            }
            return Ok(pets);
        }

        [HttpGet("petsByTypeAndBreed")]
        public async Task<List<Pet>> FilterByTypeAndFilter(string? type = null, string? breed = null)
        {
            if(string.IsNullOrEmpty(type) && string.IsNullOrEmpty(breed))
            {
                return await _petService.GetPetsAsync();
            }

            List<Pet> matchPets = new List<Pet>();
            if(!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(breed))
            {
                matchPets = await _petService.GetPetByTypeAndBreedAsync(type, breed);
            }
            else if(!string.IsNullOrEmpty(type))
            {
                matchPets = await _petService.GetPetsByTypeAsync(type);
            }
            else if(!string.IsNullOrEmpty(breed))
            {
                matchPets = await _petService.GetPetsByBreedAsync(breed);
            }
            return matchPets;
        }

        [HttpPost("GetPetNear")]
        public async Task<List<Pet>> GetPetNear([FromBody] string location)
        {
            List<Pet> allPets = await _petService.GetPetsAsync();
            List<Pet> nearPets = _petService.GetPetsNearMe(location, allPets);
            return nearPets;
        }

        [HttpPost("SearchPets")]
        public async Task<List<Pet>> PetSearching([FromBody] string userInput)
        {

            List<Pet> petSearch = new List<Pet>();
            List<string> petIds = new List<string>();
            petIds = _petService.PetSearchingModel(userInput);

            foreach(string id in petIds)
            {
                var pet = await _petService.GetPetByPetIdAsync(id);
                petSearch.Add(pet);
            }

            return petSearch;
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> AddPet(string userId, [FromBody] Pet pet)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return BadRequest("A valid userId is required.");
            }
            pet.UserId = userId;
            var newpet = await _petService.AddPetAsync(userId, pet);
            return Ok(newpet);
        }

        [HttpPut("{petId}")]
        public async Task<ActionResult<Pet>> UpdatePet(string petId, [FromBody] Pet updatepet)
        {
            var pet = await _petService.UpdatePetAsync(petId, updatepet);
            if(pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePet(string petId)
        {
            await _petService.DeletePetAsync(petId);
            return Ok("Remove pet");
        }


    }
}
