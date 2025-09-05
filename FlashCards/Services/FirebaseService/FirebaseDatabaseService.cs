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

    public Task CreateCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Task CreateCard(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>> GetCardsCollectionByCategroryId(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(string categoryId, string newName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCard(string cardId, SingleCard newCard)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCard(string cardId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SingleCard>> GetCardsCollection()
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetCategoriesCollection()
    {
        throw new NotImplementedException();
    }
}