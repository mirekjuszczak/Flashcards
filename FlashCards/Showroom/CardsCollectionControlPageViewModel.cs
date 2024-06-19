using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollectionControlPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public Task<List<SingleCard>> CardsCollection { get; }

    public CardsCollectionControlPageViewModel(IDatabaseServiceMock databaseServiceMock)
    {
        CardsCollection = databaseServiceMock.GetData();
        Title = "Two Sides Card Control Sample";
    }
}