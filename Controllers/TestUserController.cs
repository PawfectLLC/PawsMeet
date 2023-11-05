using Microsoft.AspNetCore.Mvc;
using Nest;
using PawfectAppCore.Models;

namespace PawfectAppCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestUserController : Controller
    {
        private readonly IElasticClient _elasticClient;
        public TestUserController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet("{id}")]
        public async Task<TestUser> Index(string id)
        {
            var response = await _elasticClient.GetAsync<TestUser>(new DocumentPath<TestUser>(
                new Id(id)), x => x.Index("user"));


            return response?.Source;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestUser()
        {
            var searchResponse = await _elasticClient.SearchAsync<TestUser>(s => s
            .Query(q => q.MatchAll())
            .Size(100));

            var results = await _elasticClient.SearchAsync<TestUser>(u => u.Query(
                q => q.MatchAll()));

            /*
            if (searchResponse.IsValid)
            {
                var firstUser = searchResponse.Documents;
                if (firstUser != null)
                {
                    return Ok(firstUser);
                }
                else
                {
                    return NotFound("No TestUser found.");
                }
            }
            else
            {
                return StatusCode((int)searchResponse.ApiCall.HttpStatusCode, searchResponse.DebugInformation);
            }
            */
            return Ok(results.Documents);
        }
    }
}
