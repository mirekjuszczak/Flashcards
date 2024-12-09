using FlashCards.Models;

namespace FlashCards.Services.DatabaseService;

public interface IDatabaseService
{
    Task CreateCategory(string name);
    Task CreateCard(User user);
    Task<Category> GetCategory(string name);
    Task<List<SingleCard>> GetCardsCollectionByCategroryId(string id);
    Task UpdateCategory(string categoryId, string newName);
    Task UpdateCard(string cardId, SingleCard newCard);
    Task DeleteCategory(string categoryId);
    Task DeleteCard(string cardId);
    
    Task<List<SingleCard>> GetCardsCollection();
    Task<List<Category>> GetCategoriesCollection();
}