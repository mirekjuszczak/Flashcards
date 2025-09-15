using FlashCards.Models;
using FlashCards.Models.Dto;

namespace FlashCards.Services.FlashcardsDataService;

/// <summary>
/// Service that manages FlashcardsData container and synchronizes with Firestore
/// Provides local-first approach with background synchronization
/// </summary>
public interface IFlashcardsDataService
{
    /// <summary>
    /// The main data container with Cards and Categories
    /// </summary>
    FlashcardsData Data { get; }
    
    /// <summary>
    /// Indicates if data has been loaded from the database
    /// </summary>
    bool IsLoaded { get; }
    
    /// <summary>
    /// Loads all data from Firestore into local FlashcardsData container
    /// Call this once on app startup
    /// </summary>
    Task<bool> LoadDataAsync();
    
    /// <summary>
    /// Refreshes data from Firestore (in case of conflicts or updates)
    /// </summary>
    Task<bool> RefreshDataAsync();
    
    /// <summary>
    /// Adds a new card locally and syncs to Firestore
    /// </summary>
    Task<bool> AddCardAsync(SingleCard card);
    
    /// <summary>
    /// Updates a card locally and syncs to Firestore
    /// </summary>
    Task<bool> UpdateCardAsync(SingleCard card);
    
    /// <summary>
    /// Deletes a card locally and syncs to Firestore
    /// </summary>
    Task<bool> DeleteCardAsync(string cardId);
    
    /// <summary>
    /// Deletes all cards locally and syncs to Firestore
    /// >0 - number of deleted cards; -1 - error occurred
    /// </summary>
    Task<int> DeleteAllCardsAsync();
    
    /// <summary>
    /// Updates card category locally and syncs to Firestore
    /// </summary>
    Task<bool> UpdateCardCategoryAsync(string cardId, string newCategoryId);
    
    /// <summary>
    /// Adds a new category locally and syncs to Firestore
    /// </summary>
    Task<bool> AddCategoryAsync(string categoryName);
    
    /// <summary>
    /// Updates category name locally and syncs to Firestore
    /// </summary>
    Task<bool> UpdateCategoryAsync(string categoryId, string newName);
    
    /// <summary>
    /// Deletes a category locally and syncs to Firestore
    /// </summary>
    Task<bool> DeleteCategoryAsync(string categoryId);
    
    /// <summary>
    /// Deletes all categories locally and syncs to Firestore
    /// >0 - number of deleted cards; -1 - error occurred
    /// </summary>
    Task<int> DeleteAllCategoriesAsync();
    
    // DTO Methods for ViewModels
    /// <summary>
    /// Gets all cards as DTOs with category information
    /// </summary>
    List<SingleCardDto> GetCardsAsDto();
    
    /// <summary>
    /// Gets cards by category as DTOs
    /// </summary>
    List<SingleCardDto> GetCardsByCategoryAsDto(string categoryId);
    
    /// <summary>
    /// Gets cards with undefined category as DTOs
    /// </summary>
    List<SingleCardDto> GetCardsWithUndefinedCategoryAsDto();
    
    /// <summary>
    /// Gets favorite cards as DTOs
    /// </summary>
    List<SingleCardDto> GetFavoriteCardsAsDto();
    
    /// <summary>
    /// Gets cards by learning progress as DTOs
    /// </summary>
    List<SingleCardDto> GetCardsByLearningProgressAsDto(LearningProgress progress);
}
