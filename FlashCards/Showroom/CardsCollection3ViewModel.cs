using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollection3ViewModel : BaseViewModel
{
    private readonly IDatabaseServiceMock _databaseServiceMock;
    [ObservableAsProperty] public List<SingleCard> CardsCollection { get; } = new List<SingleCard>();

    // public ObservableCollection<SingleCard> CardsCollection { get; } = new ObservableCollection<SingleCard>();

    public CardsCollection3ViewModel(IDatabaseServiceMock databaseServiceMock)
    {
        _databaseServiceMock = databaseServiceMock;
        _ = InitializeCardsCollection();
        
        Title = "Two Sides Card Collection 3 Sample";
    }

    private async Task InitializeCardsCollection()
    {
        var collection = await _databaseServiceMock.GetCardsCollection();
        CardsCollection.AddRange(collection);
    }
}