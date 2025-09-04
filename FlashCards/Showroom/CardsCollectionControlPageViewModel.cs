using FlashCards.Models;
using FlashCards.Services.DatabaseServiceMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollectionControlPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public List<SingleCard> CardsCollection { get; } = [];

    public CardsCollectionControlPageViewModel()
    {
        InitializeCardsCollection();
        
        Title = "Two Sides Card Control Sample";
    }

    private void InitializeCardsCollection()
    {
        var collection = new DatabaseServiceMock();
        CardsCollection.AddRange(collection.CardsCollection);
    }
}