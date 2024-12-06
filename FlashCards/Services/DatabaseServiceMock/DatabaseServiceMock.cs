using FlashCards.Models;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Services.DatabaseServiceMock;

public class DatabaseService : IDatabaseService
{
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

    private List<Category> CreateMockedCategoriesList()
    {
        var categories = new List<Category>();
        
        categories.Add(new Category
        {
            Id = 1.ToString(),
            Name = "Noun"
        });
        
        categories.Add(new Category
        {
            Id = 2.ToString(),
            Name = "Verb"
        });
        
        categories.Add(new Category
        {
            Id = 3.ToString(),
            Name = "Airport"
        });

        categories.Add(new Category
        {
            Id = 4.ToString(),
            Name = "Adjective"
        });
        
        categories.Add(new Category
        {
            Id = 5.ToString(),
            Name = "Restaurant"
        });
        
        categories.Add(new Category
        {
            Id = 6.ToString(),
            Name = "Hotel"
        });
        
        categories.Add(new Category
        {
            Id = 7.ToString(),
            Name = "Prepositions"
        });
        
        categories.Add(new Category
        {
            Id = 8.ToString(),
            Name = "Tenses"
        });
        
        categories.Add(new Category
        {
            Id = 9.ToString(),
            Name = "Phrasal Verbs"
        });
        
        categories.Add(new Category
        {
            Id = 10.ToString(),
            Name = "Idioms"
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
            Phrase = "Cat",
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
            Phrase = "Dog",
            Translation = "Pies",
            Example = "This is my dog",
            CategoryName = categories.GetCategoryName(1.ToString()) ?? string.Empty,
            CategoryId = 1.ToString(),
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        cards.Add(new SingleCard()
        {
            Id = "C3",
            Phrase = "run",
            Translation = "biec/biegać",
            Example = "I was running yesterday",
            CategoryName = categories.GetCategoryName(2.ToString()) ?? string.Empty,
            CategoryId = 2.ToString(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = false
        });
        
        cards.Add(new SingleCard()
        {
            Id = "C4",
            Phrase = "buy",
            Translation = "kupować",
            Example = "This is my cat",
            CategoryName = categories.GetCategoryName(2.ToString()) ?? string.Empty,
            CategoryId = 2.ToString(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        cards.Add(new SingleCard()
        {
            Id = "C5",
            Phrase = "move",
            Translation = "przemiaszczać się/przesuwać",
            Example = "We moved our flat last year",
            CategoryName = categories.GetCategoryName(2.ToString()) ?? string.Empty,
            CategoryId = 2.ToString(),
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        cards.Add(new SingleCard()
        {
            Id = "C61",
            Phrase = "Prepare",
            Translation = "Przygotowywać",
            Example = "I try to prepare this application",
            CategoryName = categories.GetCategoryName(2.ToString()) ?? string.Empty,
            CategoryId = 2.ToString(),
            LearningProgress = LearningProgress.NotStarted,
            Favourite = true
        });
        
        return cards;
    }
}