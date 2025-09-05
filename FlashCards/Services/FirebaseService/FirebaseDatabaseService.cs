using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using Plugin.Firebase.Firestore;

namespace FlashCards.Services.FirebaseService;

public class FirebaseDatabaseService : IDatabaseService
{
    private const string ProjectFirebaseId = "flashcards-mjusz";
    private const string CardsCollectionName = "cards";
    
    private readonly IFirebaseFirestore _firestore;

    public FirebaseDatabaseService(IFirebaseFirestore firestore)
    {
        Console.WriteLine("MOZU: FirebaseDatabaseService constructor init");
        
        _firestore = firestore;
        
        Console.WriteLine($"MOZU: FirebaseDatabaseService constructor _firestore created {ProjectFirebaseId}");
    }

    public Task<Category?> CreateCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetCategory(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetCategoryByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>?> GetCategoriesCollection()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCategory(string categoryId, string newName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategory(string categoryId)
    {
        throw new NotImplementedException();
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
        
            var collectionReference = _firestore.GetCollection("cards");
            await collectionReference.AddDocumentAsync(card);
        
            return true; // Successfully created
        }
        catch (Exception e)
        {
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