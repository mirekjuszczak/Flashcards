using FlashCards.Models;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Services.DataService;

/// <summary>
/// Implementation of FlashcardsDataService that manages local data container with Firestore sync
/// </summary>
public class FlashcardsDataService : IFlashcardsDataService
{
    private readonly IDatabaseService _databaseService;
    
    public FlashcardsData Data { get; private set; } = new();
    public bool IsLoaded { get; private set; }

    public FlashcardsDataService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> LoadDataAsync()
    {
        try
        {
            // Load both collections in parallel for better performance
            var cardsTask = _databaseService.GetCardsCollection();
            var categoriesTask = _databaseService.GetCategoriesCollection();
            
            await Task.WhenAll(cardsTask, categoriesTask);
            
            var cards = await cardsTask;
            var categories = await categoriesTask;
            
            if (cards == null || categories == null)
                return false;
            
            // Clear local data
            Data.Cards.Clear();
            Data.Categories.Clear();
            
            foreach (var category in categories)
                Data.Categories.Add(category);
                
            foreach (var card in cards)
                Data.Cards.Add(card);
            
            // Update category card counts based on actual data
            Data.UpdateAllCategoryCardCounts();
            
            IsLoaded = true;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> RefreshDataAsync()
    {
        return await LoadDataAsync();
    }

    public async Task<bool> AddCardAsync(SingleCard card)
    {
        try
        {
            // 1. Add to Firestore first to get generated ID
            var success = await _databaseService.CreateCard(card);
            
            if (success.HasValue && success.Value)
            {
                // 2. Add to local data (with automatic category count update)
                Data.AddCard(card);
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding card: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCardAsync(SingleCard card)
    {
        try
        {
            if (string.IsNullOrEmpty(card.Id))
                return false;
                
            // 1. Update in Firestore
            var success = await _databaseService.UpdateCard(card.Id, card);
            
            if (success)
            {
                // 2. Update local data
                var localCard = Data.Cards.FirstOrDefault(c => c.Id == card.Id);
                if (localCard != null)
                {
                    // Update properties
                    localCard.Phrase = card.Phrase;
                    localCard.Translation = card.Translation;
                    localCard.Example = card.Example;
                    localCard.LearningProgress = card.LearningProgress;
                    localCard.Favourite = card.Favourite;
                    
                    // If category changed, update counts
                    if (localCard.CategoryId != card.CategoryId)
                    {
                        Data.UpdateCardCategory(card.Id, card.CategoryId);
                    }
                }
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating card: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCardAsync(string cardId)
    {
        try
        {
            // 1. Delete from Firestore
            var success = await _databaseService.DeleteCard(cardId);
            
            if (success)
            {
                // 2. Remove from local data (with automatic category count update)
                Data.RemoveCard(cardId);
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting card: {ex.Message}");
            return false;
        }
    }

    public async Task<int> DeleteAllCardsAsync()
    {
        try
        {
            var deletingCounter = await _databaseService.DeleteAllCards();

            if (deletingCounter > 0)
            {
                Data.Cards.Clear();
                Data.UpdateAllCategoryCardCounts();
            }

            return deletingCounter;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting all cards: {e.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateCardCategoryAsync(string cardId, string newCategoryId)
    {
        try
        {
            // 1. Update in Firestore
            var success = await _databaseService.UpdateCardCategory(cardId, newCategoryId);
            
            if (success)
            {
                // 2. Update local data (with automatic category count updates)
                Data.UpdateCardCategory(cardId, newCategoryId);
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating card category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> AddCategoryAsync(string categoryName)
    {
        try
        {
            // 1. Create in Firestore first to get generated ID
            var newCategory = await _databaseService.CreateCategory(categoryName);
            
            if (newCategory != null)
            {
                // 2. Add to local data
                Data.Categories.Add(newCategory);
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCategoryAsync(string categoryId, string newName)
    {
        try
        {
            // 1. Update in Firestore
            var success = await _databaseService.UpdateCategory(categoryId, newName);
            
            if (success)
            {
                // 2. Update local data
                var localCategory = Data.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (localCategory != null)
                {
                    localCategory.Name = newName;
                    localCategory.LastModified = DateTime.Now;
                }
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCategoryAsync(string categoryId)
    {
        try
        {
            // 1. Delete from Firestore
            var success = await _databaseService.DeleteCategory(categoryId);
            
            if (success)
            {
                // 2. Remove from local data
                var localCategory = Data.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (localCategory != null)
                {
                    Data.Categories.Remove(localCategory);
                }
                
                // 3. Update cards that belonged to this category (set to undefined)
                var cardsInCategory = Data.Cards.Where(c => c.CategoryId == categoryId).ToList();
                foreach (var card in cardsInCategory)
                {
                    card.CategoryId = string.Empty; // or some "undefined" category ID
                }
                
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting category: {ex.Message}");
            return false;
        }
    }

    public async Task<int> DeleteAllCategoriesAsync()
    {
        try
        {
            // TODO Note: Cards that belonged to these categories should be handled separately in FirebaseDatabaseService!!!!!!
            var deletingCounter = await _databaseService.DeleteAllCategories();

            if (deletingCounter > 0)
            {
                Data.Categories.Clear();
                
                // TODO Note: Cards that belonged to these categories should be handled separately !!!!!!
                // This metod UpdateAllCategoryCardCounts to change
                Data.UpdateAllCategoryCardCounts();
            }

            return deletingCounter;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting all cards: {e.Message}");
            throw;
        }
    }
}
