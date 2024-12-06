using FlashCards.Models;

namespace FlashCards.Services.DatabaseService;

public interface IDatabaseService
{
    Task<List<SingleCard>> GetCardsCollection();
    Task<List<Category>> GetCategoriesCollection();
}