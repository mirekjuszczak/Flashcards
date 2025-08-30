using System;
using System.Threading.Tasks;
using FlashCards.Models;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Services.FirebaseService;

public class FirebaseAuthService : IUserService
{
    public Task CreateOrUpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser()
    {
        throw new NotImplementedException();
    }

    public Task RegisterUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LogoutUser()
    {
        throw new NotImplementedException();
    }
}