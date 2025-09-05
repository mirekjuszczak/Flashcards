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

    public Task<SingleCard?> CreateCard(SingleCard card)
    {
        throw new NotImplementedException();
    }

    public Task<SingleCard?> GetCard(string cardId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>?> GetCardsCollection()
    {
        throw new NotImplementedException();
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

    public Task<int> UpdateCardsCategory(string oldCategoryId, string newCategoryId)
    {
        throw new NotImplementedException();
    }
}