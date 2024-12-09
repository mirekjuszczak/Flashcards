using FlashCards.Models;

namespace FlashCards.Services.DatabaseService;

public interface IUserService
{
    Task CreateOrUpdateUser(User user);
    Task<User> GetUser();
    Task RegisterUser(string email, string password);
    Task LoginUser(string email, string password);
    Task LogoutUser();
}