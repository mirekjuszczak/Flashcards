using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollectionControlPageViewModel : BaseViewModel
{
    private readonly IDatabaseServiceMock _databaseServiceMock;
    [ObservableAsProperty] public List<SingleCard> CardsCollection { get; } = new List<SingleCard>();

    // public ObservableCollection<SingleCard> CardsCollection { get; } = new ObservableCollection<SingleCard>();

    public CardsCollectionControlPageViewModel(IDatabaseServiceMock databaseServiceMock)
    {
        _databaseServiceMock = databaseServiceMock;
        _ = InitializeCardsCollection();
        
        Title = "Two Sides Card Control Sample";
    }

    private async Task InitializeCardsCollection()
    {
        var collection = await _databaseServiceMock.GetCardsCollection();
        CardsCollection.AddRange(collection);
    }
}