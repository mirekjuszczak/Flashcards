using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.DataService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCardsCollectionPageViewModel : BaseViewModel
{
    private readonly IFlashcardsDataService _flashcardsDataService;

    public ObservableCollection<SingleCard> Cards => _flashcardsDataService.Data.Cards;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _infoText = "Click and get cards from Firestore";
    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCardsCollectionPageViewModel(IFlashcardsDataService flashcardsDataService)
    {
        _flashcardsDataService = flashcardsDataService;
        Title = "Cards collection of Firebase";

        LoadCardsCommand = new Command(async () => await LoadCardsAsync());
        DeleteCardsCollectionCommand = new Command(async () => await DeleteCardsCollectionAsync());
        AddSampleCardCommand = new Command(async () => await AddSampleCardAsync());
    }

    public Command LoadCardsCommand { get; }
    public Command DeleteCardsCollectionCommand { get; }
    public Command AddSampleCardCommand { get; }

    private async Task LoadCardsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;
        InfoText = "Getting cards from Firestore...";
        
        if (!_flashcardsDataService.IsLoaded)
        {
            var loadResult = await _flashcardsDataService.LoadDataAsync();
            if (!loadResult)
            {
                ErrorMessage = "Failed to load data from database";
                InfoText = "Error occurred while loading data";
                IsLoading = false;
                return;
            }
        }
        
        var cardsCount = _flashcardsDataService.Data.Cards.Count;

        InfoText = cardsCount > 0 
            ? $"{cardsCount} cards loaded from data service"
            : "No card in collection. Add a few cards to the collection at the beginning.";

        IsLoading = false;
    }

    private async Task DeleteCardsCollectionAsync()
    {
        IsLoading = true;
        
        var deletedCount = 0;
        var cardsToDelete = _flashcardsDataService.Data.Cards.ToList();
        
        foreach (var card in cardsToDelete)
        {
            if (card.Id != null && await _flashcardsDataService.DeleteCardAsync(card.Id))
            {
                deletedCount++;
            }
        }

        InfoText = deletedCount switch
        {
            > 0 => $"{deletedCount} cards deleted from Firestore",
            0 => "No cards were deleted",
            _ => "Error occurred while deleting cards"
        };
        
        IsLoading = false;
    }

    private async Task AddSampleCardAsync()
    {
        IsLoading = true;
        InfoText = "Adding a card as an example...";

        var ticksString = DateTime.Now.Ticks.ToString();
        var phraseSufix = ticksString.Substring(ticksString.Length - 3);

        var sampleCard = new SingleCard
        {
            Phrase = $"Hello {phraseSufix}",
            Translation = $"Czesc {phraseSufix}",
            Example = "Hello, how are you?",
            CategoryId = "undefined", // Use a default category
            Favourite = false,
            LearningProgress = LearningProgress.NotStarted
        };

        var addingResult = await _flashcardsDataService.AddCardAsync(sampleCard);

        InfoText = addingResult switch
        {
            true => "Card added to Firestore and local data",
            false => "Card not added. Validation failed or error occurred."
        };

        await LoadCardsAsync();
        IsLoading = false;
    }
}