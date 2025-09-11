using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using Plugin.Firebase.Firestore;

namespace FlashCards.Services.FirebaseService;

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

    public Task<Category?> GetCategory(string categoryId)
    {
        throw new NotImplementedException();
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

    public Task<bool> UpdateCategory(string categoryId, string newName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategory(string categoryId)
    {
        throw new NotImplementedException();
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
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteDocumentAsync();
                    deletingCounter++;
                    
                    // TODO Note: Cards that belonged to these categories should be handled separately !!!!!!
                }
            }
            
            return deletingCounter; // Return number of deleted categories (0 if no cards)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error deleting all cards: {ex.Message}");
            return -1; // -1 indicates error according to interface convention
        }
    }

    public Task<bool?> CategoryExists(string categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool?> CreateCard(SingleCard card)
    {
        try
        {
            // Validation - return false for invalid input (not exceptions)
            if (string.IsNullOrWhiteSpace(card.Phrase) || string.IsNullOrWhiteSpace(card.Translation))
                return false;
        
            var collectionReference = _firestore.GetCollection(CardsCollectionName);
            await collectionReference.AddDocumentAsync(card);
        
            return true; // Successfully created
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error creating card: {ex.Message}");
            return null;
        }
    }

    public Task<SingleCard?> GetCard(string cardId)
    {
        throw new NotImplementedException();
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
                    // Convert a document form Firebase to SingleCard
                    var card = document.Data;
                    if (card != null)
                    {
                        cards.Add(card);
                    }
                }
            }
            
            return cards; // empty list if na cards
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return null; // null - Error
        }
    }

    public Task<List<SingleCard>?> GetCardsCollectionByCategroryId(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>?> GetCardsWithUndefinedCategory()
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>?> GetFavoriteCards()
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>?> GetCardsByLearningProgress(LearningProgress progress)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCard(string cardId, SingleCard updatedCard)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCardCategory(string cardId, string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetCardCategoryToUndefined(string cardId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCardLearningProgress(string cardId, LearningProgress progress)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ToggleCardFavorite(string cardId)
    {
        throw new NotImplementedException();
    }
    
    public Task<bool> DeleteCard(string cardId)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAllCardsInCategory(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>?> CreateMultipleCards(List<SingleCard> cards)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteMultipleCards(List<string> cardIds)
    {
        throw new NotImplementedException();
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

    public Task<int> UpdateCardsCategory(string oldCategoryId, string newCategoryId)
    {
        throw new NotImplementedException();
    }
}