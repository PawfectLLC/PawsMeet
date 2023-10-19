using Microsoft.AspNetCore.Mvc;
using PawfectAppCore.Models;
using PawfectAppCore.Servers;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PawfectAppCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUserController : ControllerBase
    {
        private readonly IBaseUserService _baseUserService;
        //private readonly IFirebaseAuthClient _firebaseAuthClient;
        private const string API_KEY = "AIzaSyBYqJhHSxn1wcQgZYjH97A8l1xRxGc_jRE";
        public BaseUserController(IBaseUserService baseUserService)
        {
            this._baseUserService = baseUserService;
            //this._firebaseAuthClient = firebaseAuthClient;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignUp signUp)
        {
            try
            {
                var userId = await _baseUserService.SignupBaseUser(signUp);
                return Ok(userId);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var isAuthenticated = await _baseUserService.LoginBaseUserAsync(login);
            if(isAuthenticated)
            {
                return Ok("Login");
            }
            return Unauthorized("Invalid user");
        }
        

        // GET: api/<BaseUserController>
        [HttpGet]
        public ActionResult<List<Guest>> Get()
        {
            return _baseUserService.GetAllGuests();
        }

        // GET api/<BaseUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BaseUserController>
        [HttpPost]
        public void Post([FromBody] Guest guest)
        {
            _baseUserService.AddGuest(guest);

        }

        // PUT api/<BaseUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BaseUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
