using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using Google.Cloud.Firestore;

namespace FlashCards.Services.FirebaseService;

public class FirebaseAuthDatabaseService : IDatabaseService, IUserService
{
    private const string ProjectFirebaseId = "flashcards-mjusz";
    private const string CardsCollectionName = "cards";
    
    private readonly FirestoreDb _firestore;

    public FirebaseAuthDatabaseService()
    {
        _firestore = FirestoreDb.Create(ProjectFirebaseId);
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

    public Task CreateOrUpdateUSer(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser()
    {
        throw new NotImplementedException();
    }

    public Task RegisterUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LogoutUser()
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> TestFirestoreConnection()
    {
        try
        {
            var singleCard = new SingleCard
            {
                Phrase = "Hello",
                Translation = "Czesc",
                Example = "Hello, how are you?",
                CategoryId = "1",
                LearningProgress = LearningProgress.InProgress,
                Favourite = false
            };
            
            var collection = _firestore.Collection(CardsCollectionName);
            var document = collection.Document(singleCard.Id);
            await document.SetAsync(singleCard);
            
            Console.WriteLine("\nMOZU Dodano karte testowa do kolekcji {Collection}\n", [CardsCollectionName]);
            Console.WriteLine("\nMOZU Id={Id}", [singleCard.Id]);
            Console.WriteLine("\nMOZU Id={Phrase}", [singleCard.Phrase]);
            Console.WriteLine("\nMOZU Id={Example}", [singleCard.Example]);
            Console.WriteLine("\nMOZU Id={CategoryId}", [singleCard.CategoryId]);
            Console.WriteLine("\nMOZU Id={LearningProgress}", [singleCard.LearningProgress]);
            Console.WriteLine("\nMOZU Id={Favourite}", [singleCard.Favourite]);
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MOZU Błąd podczas łączenia z Firestore: {ex.Message}");
            return false;
        }
    }
}