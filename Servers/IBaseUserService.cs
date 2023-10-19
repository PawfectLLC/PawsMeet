using PawfectAppCore.Models;
using System.ComponentModel.DataAnnotations;

namespace PawfectAppCore.Servers
{
    public interface IBaseUserService
    {
        Task<string> SignupBaseUser(SignUp signUp);
        Task<bool> LoginBaseUserAsync(Login login);
        List<Guest> GetAllGuests();
        //Guest GetGuestByuserId(int id);
        Guest AddGuest(Guest guest);
        Task<string> getUserIdByEmailPassword(string email, string password);
    }
}
