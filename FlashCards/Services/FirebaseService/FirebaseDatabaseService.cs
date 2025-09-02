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
        var listCards = CreateMockedCardsList();

        return Task.FromResult(listCards);
    }

    public Task<List<Category>> GetCategoriesCollection()
    {
        var listCategories = CreateMockedCategoriesList();

        return Task.FromResult(listCategories);
    }
    
    public async Task<bool> TestFirestoreConnection()
    {
        try
        {
            var singleCard = new SingleCard
            {
                Phrase = "Hello FirebaseTest",
                Translation = "Czesc",
                Example = "Hello, how are you?",
                CategoryId = "1",
                LearningProgress = LearningProgress.InProgress,
                Favourite = false
            };
            
            // Correct way to add a document using Plugin.Firebase
            var collection = _firestore.GetCollection(CardsCollectionName);
            var documentReference = await collection.AddDocumentAsync(singleCard);
            
            Console.WriteLine($"\nMOZU: Dodano karte testowa do kolekcji {CardsCollectionName}\n");
            Console.WriteLine($"\nMOZU: DocumentId={documentReference.Id}");
            Console.WriteLine($"\nMOZU: Id={singleCard.Id}");
            Console.WriteLine($"\nMOZU: Phrase={singleCard.Phrase}");
            Console.WriteLine($"\nMOZU: Example={singleCard.Example}");
            Console.WriteLine($"\nMOZU: CategoryId={singleCard.CategoryId}");
            Console.WriteLine($"\nMOZU: LearningProgress={singleCard.LearningProgress}");
            Console.WriteLine($"\nMOZU: Favourite={singleCard.Favourite}");
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MOZU: Błąd podczas łączenia z Firestore: {ex.Message}");
            Console.WriteLine($"MOZU: StackTrace: {ex.StackTrace}");
            return false;
        }
    }
    
     private List<Category> CreateMockedCategoriesList()
    {
        var categories = new List<Category>();
        
        categories.Add(new Category
        {
            Id = 1.ToString(),
            Name = "Noun FirebaseTest"
        });
        
        categories.Add(new Category
        {
            Id = 2.ToString(),
            Name = "Verb FirebaseTest"
        });
        
        categories.Add(new Category
        {
            Id = 3.ToString(),
            Name = "Airport FirebaseTest"
        });
        
        return categories;
    }

    private List<SingleCard> CreateMockedCardsList()
    {
        var categories = CreateMockedCategoriesList();
        
        var cards = new List<SingleCard>();

        cards.Add(new SingleCard
        {
            Id = "C1",
            Phrase = "Cat FirebaseTest",
            Translation = "Kot",
            Example = "This is my cat",
            CategoryName = categories.GetCategoryName(1.ToString()) ?? string.Empty,
            CategoryId = 1.ToString(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        cards.Add(new SingleCard()
        {
            Id = "C2",
            Phrase = "Dog FirebaseTest",
            Translation = "Pies",
            Example = "This is my dog",
            CategoryName = categories.GetCategoryName(1.ToString()) ?? string.Empty,
            CategoryId = 1.ToString(),
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        return cards;
    }
}