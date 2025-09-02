using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using Plugin.Firebase.Firestore;

namespace FlashCards.Services.FirebaseService;

public class FirebaseDatabaseService : IDatabaseService
{
    private const string ProjectFirebaseId = "flashcards-mjusz";
    private const string CardsCollectionName = "cards";
    
    private readonly IFirebaseFirestore _firebase;

    public FirebaseDatabaseService()
    {
        Console.WriteLine("MOZUTEST FirebaseDatabaseService constructor init");
        
        _firebase = CrossFirebaseFirestore.Current;
        
        Console.WriteLine($"MOZUTEST FirebaseDatabaseService constructor _firestore created {ProjectFirebaseId}");
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
            
            // var collection = _firestore.Collection(CardsCollectionName);
            // var document = collection.Document(singleCard.Id);
            // await document.SetAsync(singleCard);
            
            var collection = _firebase.GetCollection(CardsCollectionName);
            var document = collection.AddDocumentAsync(singleCard);
            
            Console.WriteLine($"\nMOZUTEST Dodano karte testowa do kolekcji {CardsCollectionName}\n");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.Id}");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.Phrase}");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.Example}");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.CategoryId}");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.LearningProgress}");
            Console.WriteLine($"\nMOZUTEST Id={singleCard.Favourite}");
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MOZUTEST Błąd podczas łączenia z Firestore: {ex.Message}");
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