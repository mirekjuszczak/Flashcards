using FlashCards.Models;
using FlashCards.Models.Dto;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Services.FlashcardsDataService;

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
                // 2. Add to local data
                Data.AddCard(card);
                
                // 3. Synchronize category metadata from Firestore
                await UpdateCountCardAndDateInCategoryWithDataBase(card);
                
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
                    var oldCategoryId = localCard.CategoryId;
                    
                    // Update properties
                    localCard.Phrase = card.Phrase;
                    localCard.Translation = card.Translation;
                    localCard.Example = card.Example;
                    localCard.LearningProgress = card.LearningProgress;
                    localCard.Favourite = card.Favourite;
                    localCard.CategoryId = card.CategoryId;
                    
                    // If category changed, synchronize both categories
                    if (oldCategoryId != card.CategoryId)
                    {
                        // Sync old category if it existed
                        if (!string.IsNullOrEmpty(oldCategoryId))
                        {
                            await UpdateCountCardAndDateInCategoryWithDataBase(new SingleCard { CategoryId = oldCategoryId });
                        }
                        
                        // Sync new category
                        await UpdateCountCardAndDateInCategoryWithDataBase(card);
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
            // Get old category before update
            var oldCard = Data.Cards.FirstOrDefault(c => c.Id == cardId);
            var oldCategoryId = oldCard?.CategoryId;
            
            // 1. Update in Firestore
            var success = await _databaseService.UpdateCardCategory(cardId, newCategoryId);
            
            if (success)
            {
                // 2. Update local data
                Data.UpdateCardCategory(cardId, newCategoryId);
                
                // 3. Synchronize both old and new categories
                if (!string.IsNullOrEmpty(oldCategoryId))
                {
                    await UpdateCountCardAndDateInCategoryWithDataBase(new SingleCard { CategoryId = oldCategoryId });
                }
                
                if (!string.IsNullOrEmpty(newCategoryId))
                {
                    await UpdateCountCardAndDateInCategoryWithDataBase(new SingleCard { CategoryId = newCategoryId });
                }
                
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
            // 1. Delete from Firestore (this also updates all related cards)
            var success = await _databaseService.DeleteCategory(categoryId);
            
            if (success)
            {
                // 2. Update local data
                var localCategory = Data.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (localCategory != null)
                {
                    Data.Categories.Remove(localCategory);
                }
                
                // Update all cards that had this category to undefined
                var cardsInCategory = Data.Cards.Where(c => c.CategoryId == categoryId).ToList();
                foreach (var card in cardsInCategory)
                {
                    card.CategoryId = string.Empty;
                }
                
                // No need to update counts - category is deleted
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
            var deletingCounter = await _databaseService.DeleteAllCategories();

            if (deletingCounter > 0)
            {
                Data.Categories.Clear();
                
                // Update all cards to have undefined category
                foreach (var card in Data.Cards)
                {
                    card.CategoryId = string.Empty;
                }
                
                // No need to update counts - all categories are deleted
            }

            return deletingCounter;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting all categories: {ex.Message}");
            return -1;
        }
    }

    // DTO Methods for ViewModels
    public List<SingleCardDto> GetCardsAsDto()
    {
        return Data.Cards.Select(card => 
        {
            var category = Data.Categories.FirstOrDefault(c => c.Id == card.CategoryId);
            return card.FromCardAndCategory(category);
        }).ToList();
    }

    public List<SingleCardDto> GetCardsByCategoryAsDto(string categoryId)
    {
        return Data.Cards
            .Where(card => card.CategoryId == categoryId)
            .Select(card => 
            {
                var category = Data.Categories.FirstOrDefault(c => c.Id == card.CategoryId);
                return card.FromCardAndCategory(category);
            }).ToList();
    }

    public List<SingleCardDto> GetCardsWithUndefinedCategoryAsDto()
    {
        return Data.Cards
            .Where(card => string.IsNullOrEmpty(card.CategoryId))
            .Select(card => card.FromCardAndCategory(null))
            .ToList();
    }

    public List<SingleCardDto> GetFavoriteCardsAsDto()
    {
        return Data.Cards
            .Where(card => card.Favourite)
            .Select(card => 
            {
                var category = Data.Categories.FirstOrDefault(c => c.Id == card.CategoryId);
                return card.FromCardAndCategory(category);
            }).ToList();
    }

    public List<SingleCardDto> GetCardsByLearningProgressAsDto(LearningProgress progress)
    {
        return Data.Cards
            .Where(card => card.LearningProgress == progress)
            .Select(card => 
            {
                var category = Data.Categories.FirstOrDefault(c => c.Id == card.CategoryId);
                return card.FromCardAndCategory(category);
            }).ToList();
    }
    
    private async Task UpdateCountCardAndDateInCategoryWithDataBase(SingleCard card)
    {
        if (!string.IsNullOrEmpty(card.CategoryId))
        {
            var updatedCategory = await _databaseService.GetCategory(card.CategoryId);
            if (updatedCategory != null)
            {
                var localCategory = Data.Categories.FirstOrDefault(c => c.Id == card.CategoryId);
                if (localCategory != null)
                {
                    localCategory.CountCards = updatedCategory.CountCards;
                    localCategory.LastModified = updatedCategory.LastModified;
                }
            }
        }
    }
}
