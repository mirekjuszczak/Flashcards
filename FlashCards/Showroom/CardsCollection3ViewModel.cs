using System.Collections.Generic;
using System.Threading.Tasks;
using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CardsCollection3ViewModel : BaseViewModel
{
    private readonly IDatabaseService _databaseService;
    [ObservableAsProperty] public List<SingleCard> CardsCollection { get; } = new List<SingleCard>();

    // public ObservableCollection<SingleCard> CardsCollection { get; } = new ObservableCollection<SingleCard>();

    public CardsCollection3ViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
        _ = InitializeCardsCollection();
        
        Title = "Two Sides Card Collection 3 Sample";
    }

    private async Task InitializeCardsCollection()
    {
        //var collection = await _databaseService.GetCardsCollection();
        //CardsCollection.AddRange(collection);

        var testFirestoreConnection = await _databaseService.TestFirestoreConnection();
    }
}