using FlashCards.Models;
using FlashCards.Models.Dto;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class TwoSidesCardControlPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public SingleCardDto Card { get; }

    public TwoSidesCardControlPageViewModel()
    {
        Card = CreateSingleCardForTesting();
        
        Title = "Two Sides Card Control Sample";
    }

    private static SingleCardDto CreateSingleCardForTesting()
    {
        return new SingleCardDto
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