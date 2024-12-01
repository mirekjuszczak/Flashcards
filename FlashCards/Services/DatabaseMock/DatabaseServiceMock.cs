using FlashCards.Models;

namespace FlashCards.Services.DatabaseMock;

public interface IDatabaseServiceMock
{
    Task<List<SingleCard>> GetData();
}

public class DatabaseServiceMock : IDatabaseServiceMock
{
    public Task<List<SingleCard>> GetData()
    {
        var listCards = CreateMockedCardsList();

        return Task.FromResult(listCards);
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