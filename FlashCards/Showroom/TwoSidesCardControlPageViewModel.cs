using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class TwoSidesCardControlPageViewModel : BaseViewModel
{
    //Cards Collectio Test
    [ObservableAsProperty] public Task<List<SingleCard>> CardsCollection { get; }
    //Single Card Test
    [ObservableAsProperty] public SingleCard Card { get; }

    public TwoSidesCardControlPageViewModel(IDatabaseServiceMock databaseServiceMock)
    {
        CardsCollection = databaseServiceMock.GetData();

        Card = CreateSingleCardForTesting();
        
        Title = "Two Sides Card Control Sample";
    }

    private static SingleCard CreateSingleCardForTesting()
    {
        return new SingleCard
        {
            Phrase = "Cat",
            Translation = "Kot",
            Example = "This is my cat",
            Category = Categories.Noun,
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        };
    }
}