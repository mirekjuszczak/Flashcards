using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using Plugin.Firebase.Firestore;

namespace FlashCards.Services.FirebaseService.FirestoreDatabaseService;

public class FirebaseDatabaseService : IDatabaseService
{
    private const string ProjectFirebaseId = "flashcards-mjusz";
    private const string CardsCollectionName = "cards";
    private const string CategoriesCollectionName = "categories";
    
    private readonly IFirebaseFirestore _firestore;

    public FirebaseDatabaseService(IFirebaseFirestore firestore)
    {
        Console.WriteLine("MOZU: FirebaseDatabaseService constructor init");
        
        _firestore = firestore;
        
        Console.WriteLine($"MOZU: FirebaseDatabaseService constructor _firestore created {ProjectFirebaseId}");
    }

    public async Task<Category?> CreateCategory(string name)
    {
        try
        {
            // Validation - return null for invalid input (not exceptions)
            if (string.IsNullOrWhiteSpace(name))
                return null;
            
            // Check if category with this name already exists
            var existingCategory = await GetCategoryByName(name);
            if (existingCategory != null)
            {
                Console.WriteLine($"Category with name '{name}' already exists");
                return null;
            }
        
            var newCategory = new Category
            {
                Name = name,
                CountCards = 0,
                LastModified = DateTime.Now
            };
        
            var collectionReference = _firestore.GetCollection(CategoriesCollectionName);
            var documentReference = await collectionReference.AddDocumentAsync(newCategory);
        
            // Retrieve the created category with its generated ID
            var createdCategorySnapshot = await documentReference.GetDocumentSnapshotAsync<Category>();

            if (createdCategorySnapshot?.Data != null)
            {
                return createdCategorySnapshot.Data; // Return the created category with ID
            }
            
            // If data is null, return null indicating failure
            Console.WriteLine("Warning: Category was not properly created or retrieved from Firebase");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error creating category: {ex.Message}");
            return null;
        }
    }

    public async Task<Category?> GetCategory(string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return null;
                
            var documentRef = _firestore.GetCollection(CategoriesCollectionName).GetDocument(categoryId);
            var snapshot = await documentRef.GetDocumentSnapshotAsync<Category>();
            
            return snapshot?.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting category: {ex.Message}");
            return null;
        }
    }

    public async Task<Category?> GetCategoryByName(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
                
            var collectionRef = _firestore.GetCollection(CategoriesCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<Category>();
            
            // Search through all categories to find one with matching name
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var category = document.Data;
                    if (category != null && string.Equals(category.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return category; // Found matching category
                    }
                }
            }
            
            return null; // Category not found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting category by name: {ex.Message}");
            return null;
        }
    }

    public async Task<List<Category>?> GetCategoriesCollection()
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CategoriesCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<Category>();
            
            var categories = new List<Category>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    // Convert a document from Firebase to Category
                    var category = document.Data;
                    if (category != null)
                    {
                        categories.Add(category);
                    }
                }
            }
            
            return categories; // empty list if no categories
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return null; // null - Error
        }
    }

    public async Task<bool> UpdateCategory(string categoryId, string newName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId) || string.IsNullOrWhiteSpace(newName))
                return false;
                
            // Check if category exists
            var existingCategory = await GetCategory(categoryId);
            if (existingCategory == null)
                return false;
                
            // Check if another category with this name already exists
            var categoryWithSameName = await GetCategoryByName(newName);
            if (categoryWithSameName != null && categoryWithSameName.Id != categoryId)
                return false;
                
            var documentRef = _firestore.GetCollection(CategoriesCollectionName).GetDocument(categoryId);
            
            var updates = new Dictionary<object, object>
            {
                ["name"] = newName,
                ["lastmodified"] = DateTime.Now
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCategory(string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return false;
                
            // Check if category exists
            var existingCategory = await GetCategory(categoryId);
            if (existingCategory == null)
                return false;
                
            // First, update all cards that use this category to have undefined category
            await UpdateCardsCategory(categoryId, string.Empty);
            
            // Then delete the category
            var documentRef = _firestore.GetCollection(CategoriesCollectionName).GetDocument(categoryId);
            await documentRef.DeleteDocumentAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting category: {ex.Message}");
            return false;
        }
    }

    public async Task<int> DeleteAllCategories()
    {
        try
        {
            var collectionReference = _firestore.GetCollection(CategoriesCollectionName);
            var querySnapshot = await collectionReference.GetDocumentsAsync<Category>();
                
            var deletingCounter = 0;
                
            if (querySnapshot.Documents?.Any() == true)
            {
                // First, set all cards to undefined category
                foreach (var document in querySnapshot.Documents)
                {
                    var category = document.Data;
                    if (category?.Id != null)
                    {
                        await UpdateCardsCategory(category.Id, string.Empty);
                    }
                }
                
                // Then delete all categories
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteDocumentAsync();
                    deletingCounter++;
                }
            }
            
            return deletingCounter; // Return number of deleted categories
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting all categories: {ex.Message}");
            return -1; // -1 indicates error according to interface convention
        }
    }

    public async Task<bool?> CategoryExists(string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return false;
                
            var category = await GetCategory(categoryId);
            return category != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error checking if category exists: {ex.Message}");
            return null;
        }
    }

    public async Task<bool?> CreateCard(SingleCard card)
    {
        try
        {
            // Validation - return false for invalid input (not exceptions)
            if (string.IsNullOrWhiteSpace(card.Phrase) || string.IsNullOrWhiteSpace(card.Translation))
                return false;
            
            string targetCategoryId = card.CategoryId;
            
            // Validate CategoryId if provided - ensure category exists
            if (!string.IsNullOrEmpty(card.CategoryId))
            {
                var categoryExists = await CategoryExists(card.CategoryId);
                if (categoryExists != true) // null or false
                {
                    Console.WriteLine($"Warning: Category {card.CategoryId} does not exist. Card will be created with undefined category.");
                    targetCategoryId = string.Empty; // Set to undefined category
                    card.CategoryId = string.Empty;
                }
            }
        
            var collectionReference = _firestore.GetCollection(CardsCollectionName);
            await collectionReference.AddDocumentAsync(card);
            
            // Update category CountCards and LastModified if card has a valid category
            if (!string.IsNullOrEmpty(targetCategoryId))
            {
                _ = await UpdateCategoryCardCountAndDate(targetCategoryId, 1);
            }
        
            return true; // Successfully created
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error creating card: {ex.Message}");
            return null;
        }
    }

    public async Task<SingleCard?> GetCard(string cardId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId))
                return null;
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            var snapshot = await documentRef.GetDocumentSnapshotAsync<SingleCard>();
            
            return snapshot?.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting card: {ex.Message}");
            return null;
        }
    }

    public async Task<List<SingleCard>?> GetCardsCollection()
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var cards = new List<SingleCard>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null)
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards; // empty list if no cards
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return null; // null - Error
        }
    }

    public async Task<List<SingleCard>?> GetCardsCollectionByCategroryId(string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return new List<SingleCard>(); // Return empty list for invalid input
                
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var cards = new List<SingleCard>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && card.CategoryId == categoryId)
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting cards by category: {ex.Message}");
            return null;
        }
    }

    public async Task<List<SingleCard>?> GetCardsWithUndefinedCategory()
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var cards = new List<SingleCard>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && string.IsNullOrEmpty(card.CategoryId))
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting cards with undefined category: {ex.Message}");
            return null;
        }
    }

    public async Task<List<SingleCard>?> GetFavoriteCards()
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var cards = new List<SingleCard>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && card.Favourite)
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting favorite cards: {ex.Message}");
            return null;
        }
    }

    public async Task<List<SingleCard>?> GetCardsByLearningProgress(LearningProgress progress)
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var cards = new List<SingleCard>();
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && card.LearningProgress == progress)
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error getting cards by learning progress: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateCard(string cardId, SingleCard updatedCard)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId) || updatedCard == null)
                return false;
                
            // Check if card exists
            var existingCard = await GetCard(cardId);
            if (existingCard == null)
                return false;
                
            // Validate CategoryId if provided
            if (!string.IsNullOrEmpty(updatedCard.CategoryId))
            {
                var categoryExists = await CategoryExists(updatedCard.CategoryId);
                if (categoryExists != true)
                {
                    Console.WriteLine($"Warning: Category {updatedCard.CategoryId} does not exist. Setting to undefined category.");
                    updatedCard.CategoryId = string.Empty;
                }
            }
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            
            var updates = new Dictionary<object, object>
            {
                ["phrase"] = updatedCard.Phrase,
                ["translation"] = updatedCard.Translation,
                ["example"] = updatedCard.Example ?? string.Empty,
                ["categoryId"] = updatedCard.CategoryId,
                ["learningProgress"] = (int)updatedCard.LearningProgress,
                ["favourite"] = updatedCard.Favourite
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating card: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCardCategory(string cardId, string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId))
                return false;
                
            // Check if card exists
            var existingCard = await GetCard(cardId);
            if (existingCard == null)
                return false;
                
            // Validate CategoryId if provided
            if (!string.IsNullOrEmpty(categoryId))
            {
                var categoryExists = await CategoryExists(categoryId);
                if (categoryExists != true)
                {
                    Console.WriteLine($"Warning: Category {categoryId} does not exist. Setting to undefined category.");
                    categoryId = string.Empty;
                }
            }
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            
            var updates = new Dictionary<object, object>
            {
                ["categoryId"] = categoryId
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating card category: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SetCardCategoryToUndefined(string cardId)
    {
        return await UpdateCardCategory(cardId, string.Empty);
    }

    public async Task<bool> UpdateCardLearningProgress(string cardId, LearningProgress progress)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId))
                return false;
                
            // Check if card exists
            var existingCard = await GetCard(cardId);
            if (existingCard == null)
                return false;
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            
            var updates = new Dictionary<object, object>
            {
                ["learningProgress"] = (int)progress
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating card learning progress: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ToggleCardFavorite(string cardId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId))
                return false;
                
            // Get current card to toggle favorite status
            var existingCard = await GetCard(cardId);
            if (existingCard == null)
                return false;
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            
            var updates = new Dictionary<object, object>
            {
                ["favourite"] = !existingCard.Favourite
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error toggling card favorite: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> DeleteCard(string cardId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cardId))
                return false;
                
            // Check if card exists
            var existingCard = await GetCard(cardId);
            if (existingCard == null)
                return false;
                
            var documentRef = _firestore.GetCollection(CardsCollectionName).GetDocument(cardId);
            await documentRef.DeleteDocumentAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting card: {ex.Message}");
            return false;
        }
    }

    public async Task<int> DeleteAllCardsInCategory(string categoryId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return 0;
                
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var deletingCounter = 0;
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && card.CategoryId == categoryId)
                    {
                        await document.Reference.DeleteDocumentAsync();
                        deletingCounter++;
                    }
                }
            }
            
            return deletingCounter;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting cards in category: {ex.Message}");
            return -1;
        }
    }

    public async Task<List<SingleCard>?> CreateMultipleCards(List<SingleCard> cards)
    {
        try
        {
            if (cards == null || !cards.Any())
                return new List<SingleCard>();
                
            var createdCards = new List<SingleCard>();
            var collectionReference = _firestore.GetCollection(CardsCollectionName);
            
            foreach (var card in cards)
            {
                // Validate each card
                if (string.IsNullOrWhiteSpace(card.Phrase) || string.IsNullOrWhiteSpace(card.Translation))
                {
                    Console.WriteLine($"Skipping invalid card: {card.Phrase} - {card.Translation}");
                    continue;
                }
                
                // Validate CategoryId if provided
                if (!string.IsNullOrEmpty(card.CategoryId))
                {
                    var categoryExists = await CategoryExists(card.CategoryId);
                    if (categoryExists != true)
                    {
                        Console.WriteLine($"Warning: Category {card.CategoryId} does not exist. Card will be created with undefined category.");
                        card.CategoryId = string.Empty;
                    }
                }
                
                var documentReference = await collectionReference.AddDocumentAsync(card);
                var createdCardSnapshot = await documentReference.GetDocumentSnapshotAsync<SingleCard>();
                
                if (createdCardSnapshot?.Data != null)
                {
                    createdCards.Add(createdCardSnapshot.Data);
                }
            }
            
            return createdCards;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error creating multiple cards: {ex.Message}");
            return null;
        }
    }

    public async Task<int> DeleteMultipleCards(List<string> cardIds)
    {
        try
        {
            if (cardIds == null || !cardIds.Any())
                return 0;
                
            var deletingCounter = 0;
            
            foreach (var cardId in cardIds)
            {
                if (string.IsNullOrWhiteSpace(cardId))
                    continue;
                    
                var success = await DeleteCard(cardId);
                if (success)
                {
                    deletingCounter++;
                }
            }
            
            return deletingCounter;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting multiple cards: {ex.Message}");
            return -1;
        }
    }

    public async Task<int> DeleteAllCards()
    {
        try
        {
            var collectionReference = _firestore.GetCollection(CardsCollectionName);
            var querySnapshot = await collectionReference.GetDocumentsAsync<SingleCard>();
                
            var deletingCounter = 0;
                
            if (querySnapshot.Documents?.Any() == true)
            {
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteDocumentAsync();
                    deletingCounter++;
                }
            }
            
            return deletingCounter; // Return number of deleted cards (0 if no cards)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting all cards: {ex.Message}");
            return -1; // -1 indicates error according to interface convention
        }
    }

    public async Task<int> UpdateCardsCategory(string oldCategoryId, string newCategoryId)
    {
        try
        {
            var collectionRef = _firestore.GetCollection(CardsCollectionName);
            var snapshot = await collectionRef.GetDocumentsAsync<SingleCard>();
            
            var updatedCounter = 0;
            
            if (snapshot.Documents?.Any() == true)
            {
                foreach (var document in snapshot.Documents)
                {
                    var card = document.Data;
                    if (card != null && card.CategoryId == oldCategoryId)
                    {
                        var updates = new Dictionary<object, object>
                        {
                            ["categoryId"] = newCategoryId
                        };
                        
                        await document.Reference.UpdateDataAsync(updates);
                        updatedCounter++;
                    }
                }
            }
            
            return updatedCounter; // Return number of updated cards
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating cards category: {ex.Message}");
            return -1; // -1 indicates error
        }
    }
    
    /// <summary>
    /// Updates category's CountCards by the specified increment and sets LastModified to current time
    /// </summary>
    /// <param name="categoryId">ID of the category to update</param>
    /// <param name="increment">Number to add to CountCards (can be negative for decrement)</param>
    /// <returns>True if update was successful, false otherwise</returns>
    private async Task<bool> UpdateCategoryCardCountAndDate(string categoryId, int increment)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return false;
            
            var category = await GetCategory(categoryId);
            if (category == null)
                return false;
                
            var newCount = Math.Max(0, category.CountCards + increment);
            
            var documentRef = _firestore.GetCollection(CategoriesCollectionName).GetDocument(categoryId);
            
            var updates = new Dictionary<object, object>
            {
                ["countcards"] = newCount,
                ["lastmodified"] = DateTime.Now
            };
            
            await documentRef.UpdateDataAsync(updates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error updating category card count: {ex.Message}");
            return false;
        }
    }
}
