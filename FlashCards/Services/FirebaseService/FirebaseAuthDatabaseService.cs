using FlashCards.Models;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Services.FirebaseService;

public class FirebaseAuthDatabaseService : IDatabaseService, IUserService
{
    public Task CreateCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Task CreateCard(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>> GetCardsCollectionByCategroryId(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(string categoryId, string newName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCard(string cardId, SingleCard newCard)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCard(string cardId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>> GetCardsCollection()
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetCategoriesCollection()
    {
        throw new NotImplementedException();
    }

    public Task CreateOrUpdateUSer(User user)
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