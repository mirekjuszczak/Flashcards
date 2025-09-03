using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using FlashCards.Services.DatabaseServiceMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollectionControlPageViewModel : BaseViewModel
{
    private readonly IDatabaseService _databaseService;
    [ObservableAsProperty] public List<SingleCard> CardsCollection { get; } = new List<SingleCard>();

    public CardsCollectionControlPageViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeCardsCollection();
        
        Title = "Two Sides Card Control Sample";
    }

    private void InitializeCardsCollection()
    {
        var collection = new DatabaseServiceMock();
        CardsCollection.AddRange(collection.CardsCollection);
    }
}