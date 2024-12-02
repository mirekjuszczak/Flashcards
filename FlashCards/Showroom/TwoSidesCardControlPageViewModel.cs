using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class TwoSidesCardControlPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public SingleCard Card { get; }

    public TwoSidesCardControlPageViewModel()
    {
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
            CategoryName = "Categories Test",
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        };
    }
}