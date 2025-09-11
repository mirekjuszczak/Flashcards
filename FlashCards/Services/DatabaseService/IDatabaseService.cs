using System.Collections.Generic;
using System.Threading.Tasks;
using FlashCards.Models;

namespace FlashCards.Services.DatabaseService;

public interface IDatabaseService
{
    // CATEGORY OPERATIONS
    
    /// <summary>
    /// Creates a new category in the Firestore database
    /// </summary>
    /// <param name="name">The name of the category to create</param>
    /// <returns>The created category object with generated ID, or null if creation failed</returns>
    Task<Category?> CreateCategory(string name);

    
    /// <summary>
    /// Retrieves a category by its unique identifier
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category</param>
    /// <returns>The category object if found, null otherwise (not found or error)</returns>
    Task<Category?> GetCategory(string categoryId);
    
    /// <summary>
    /// Retrieves a category by its name
    /// </summary>
    /// <param name="name">The name of the category to find</param>
    /// <returns>The category object if found, null otherwise (not found or error)</returns>
    Task<Category?> GetCategoryByName(string name);
    
    /// <summary>
    /// Retrieves all categories from the database
    /// </summary>
    /// <returns>List of all categories (can be empty), or null if operation failed</returns>
    Task<List<Category>?> GetCategoriesCollection();
    
    /// <summary>
    /// Updates the name of an existing category
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category to update</param>
    /// <param name="newName">The new name for the category</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateCategory(string categoryId, string newName);
    
    /// <summary>
    /// Deletes a category from the database
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category to delete</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteCategory(string categoryId);
    
    /// <summary>
    /// Checks if a category exists in the database
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category</param>
    /// <returns>True if category exists, false if not exists, null if operation failed</returns>
    Task<bool?> CategoryExists(string categoryId);
    
    // CARD OPERATIONS
    
    /// <summary>
    /// Creates a new flashcard in the Firestore database
    /// </summary>
    /// <param name="card">The card object to create</param>
    /// <returns>true OK, false not added, or null if execption</returns>
    Task<bool?> CreateCard(SingleCard card);
    
    /// <summary>
    /// Retrieves a specific card by its unique identifier
    /// </summary>
    /// <param name="cardId">The unique identifier of the card</param>
    /// <returns>The card object if found, null otherwise (not found or error)</returns>
    Task<SingleCard?> GetCard(string cardId);
    
    /// <summary>
    /// Retrieves all cards from the database
    /// </summary>
    /// <returns>List of all cards (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> GetCardsCollection();
    
    /// <summary>
    /// Retrieves all cards belonging to a specific category
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category</param>
    /// <returns>List of cards in the specified category (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> GetCardsCollectionByCategroryId(string categoryId);
    
    /// <summary>
    /// Retrieves all cards that have no assigned category (undefined category)
    /// </summary>
    /// <returns>List of cards with undefined category (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> GetCardsWithUndefinedCategory();
    
    /// <summary>
    /// Retrieves all favorite cards
    /// </summary>
    /// <returns>List of cards marked as favorites (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> GetFavoriteCards();
    
    /// <summary>
    /// Retrieves cards by learning progress status
    /// </summary>
    /// <param name="progress">The learning progress to filter by</param>
    /// <returns>List of cards with the specified learning progress (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> GetCardsByLearningProgress(LearningProgress progress);
    
    /// <summary>
    /// Updates an existing card with new data
    /// </summary>
    /// <param name="cardId">The unique identifier of the card to update</param>
    /// <param name="updatedCard">The updated card data</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateCard(string cardId, SingleCard updatedCard);
    
    /// <summary>
    /// Updates the category assignment for a specific card
    /// </summary>
    /// <param name="cardId">The unique identifier of the card</param>
    /// <param name="categoryId">The new category ID to assign</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateCardCategory(string cardId, string categoryId);
    
    /// <summary>
    /// Sets a card's category to undefined/null
    /// </summary>
    /// <param name="cardId">The unique identifier of the card</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> SetCardCategoryToUndefined(string cardId);
    
    /// <summary>
    /// Updates the learning progress of a specific card
    /// </summary>
    /// <param name="cardId">The unique identifier of the card</param>
    /// <param name="progress">The new learning progress status</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateCardLearningProgress(string cardId, LearningProgress progress);
    
    /// <summary>
    /// Toggles the favorite status of a card
    /// </summary>
    /// <param name="cardId">The unique identifier of the card</param>
    /// <returns>True if toggle was successful, false otherwise</returns>
    Task<bool> ToggleCardFavorite(string cardId);
    
    /// <summary>
    /// Deletes a card from the database
    /// </summary>
    /// <param name="cardId">The unique identifier of the card to delete</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteCard(string cardId);
    
    /// <summary>
    /// Deletes all cards belonging to a specific category
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category</param>
    /// <returns>Number of cards deleted (0 or more), or -1 if operation failed</returns>
    Task<int> DeleteAllCardsInCategory(string categoryId);
    
    /// <summary>
    /// Deletes all cards from the database (WARNING: This will remove ALL cards!)
    /// </summary>
    /// <returns>Number of cards deleted (0 or more), or -1 if operation failed</returns>
    Task<int> DeleteAllCards();
    
    // BULK OPERATIONS
    
    /// <summary>
    /// Creates multiple cards in a batch operation for better performance
    /// </summary>
    /// <param name="cards">List of cards to create</param>
    /// <returns>List of created cards with generated IDs (can be empty), or null if operation failed</returns>
    Task<List<SingleCard>?> CreateMultipleCards(List<SingleCard> cards);
    
    /// <summary>
    /// Deletes multiple cards in a batch operation
    /// </summary>
    /// <param name="cardIds">List of card IDs to delete</param>
    /// <returns>Number of cards successfully deleted (0 or more), or -1 if operation failed</returns>
    Task<int> DeleteMultipleCards(List<string> cardIds);
}