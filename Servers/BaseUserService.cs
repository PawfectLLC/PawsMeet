using Amazon.Runtime.Internal;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public class BaseUserService : IBaseUserService
    {
        private readonly IMongoCollection<Guest> _guests;
        private const string API_KEY = "AIzaSyBYqJhHSxn1wcQgZYjH97A8l1xRxGc_jRE";
        public BaseUserService(MongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("BaseUsers");
            _guests = database.GetCollection<Guest>("SignUpProfile");
        }
        
        public async Task<string> SignupBaseUser(SignUp signup)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
            FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(signup.Email, signup.PasswordEmail);
            var token = firebaseAuthLink.FirebaseToken;
            var FBuserId = firebaseAuthLink.User.LocalId;

            var newGuest = new Guest
            {
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                UserId = FBuserId,
                Auth = new Auth
                {
                    Email = signup.Email,
                    PasswordEmail = signup.PasswordEmail,
                }
            };
            _guests.InsertOne(newGuest);

            return newGuest.UserId;
        }

        public async Task<bool> LoginBaseUserAsync(Login login)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
            FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
            var token = firebaseAuthLink.FirebaseToken;
            var FBuserId = firebaseAuthLink.User.LocalId;

            //var userId = getUserIdByEmailPassword(login.Email, login.Password);
            //var user = await _guests.Find(u => u.UserId == token).SingleOrDefaultAsync();

            return FBuserId != null;
        }


        public List<Guest> GetAllGuests()
        {
            return _guests.Find(user => true).ToList();
        }

        public Guest GetGuestByuserId(string id)
        {
            return _guests.Find(user => user.Id == id).FirstOrDefault();
        }

        public async Task<string> getUserIdByEmailPassword(string email, string password)
        {
            var user = await _guests.Find(u => u.Auth.Email == email && u.Auth.PasswordEmail == password).SingleOrDefaultAsync();
            if(user != null)
            {
                return user.UserId;
            }
            return null;
        }

        public Guest AddGuest(Guest guest)
        {
            _guests.InsertOne(guest);
            return guest;
        }


    }
}
