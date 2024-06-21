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
        var resultList = CreateListMocked();

        return Task.FromResult(resultList);
    }

    public List<SingleCard> GetDataAsList() => CreateListMocked();

    private static List<SingleCard> CreateListMocked()
    {
        var resultList = new List<SingleCard>();

        resultList.Add(new SingleCard()
        {
            Phrase = "Cat",
            Translation = "Kot",
            Example = "This is my cat",
            Category = Categories.Noun,
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        resultList.Add(new SingleCard()
        {
            Phrase = "Dog",
            Translation = "Pies",
            Example = "This is my dog",
            Category = Categories.Noun,
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        resultList.Add(new SingleCard()
        {
            Phrase = "run",
            Translation = "biec/biegać",
            Example = "I was running yesterday",
            Category = Categories.Verb,
            LearningProgress = LearningProgress.InProgress,
            Favourite = false
        });
        
        resultList.Add(new SingleCard()
        {
            Phrase = "buy",
            Translation = "kupować",
            Example = "This is my cat",
            Category = Categories.Noun,
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        resultList.Add(new SingleCard()
        {
            Phrase = "move",
            Translation = "przemiaszczać się/przesuwać",
            Example = "We moved our flat last year",
            Category = Categories.Verb,
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        resultList.Add(new SingleCard()
        {
            Phrase = "Prepare",
            Translation = "Przygotowywać",
            Example = "I try to prepare this application",
            Category = Categories.Verb,
            LearningProgress = LearningProgress.NotStarted,
            Favourite = true
        });
        return resultList;
    }
}