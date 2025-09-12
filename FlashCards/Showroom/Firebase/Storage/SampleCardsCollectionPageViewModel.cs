using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.DataService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCardsCollectionPageViewModel : BaseViewModel
{
    private readonly IFlashcardsDataService _flashcardsDataService;

    [ObservableProperty] private ObservableCollection<SingleCard> _cards = new();

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _infoText = "Click and get cards from Firestore";

    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCardsCollectionPageViewModel(IFlashcardsDataService flashcardsDataService)
    {
        _flashcardsDataService = flashcardsDataService;
        Title = "Cards collection of Firebase";

        LoadCardsCommand = new Command(async () => 
        {
            try
            {
                await LoadCardsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading cards: {ex.Message}";
            }
        });
        
        DeleteCardsCollectionCommand = new Command(async () => 
        {
            try
            {
                await DeleteCardsCollectionAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting cards: {ex.Message}";
            }
        });
        
        AddSampleCardCommand = new Command(async () => 
        {
            try
            {
                await AddSampleCardAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding card: {ex.Message}";
            }
        });
    }

    public Command LoadCardsCommand { get; }
    public Command DeleteCardsCollectionCommand { get; }
    public Command AddSampleCardCommand { get; }

    private async Task LoadCardsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;
        InfoText = "Getting cards from Firestore...";
        Cards.Clear();

        // Ensure data is loaded from database
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

        // Get cards from local data service
        var cards = _flashcardsDataService.Data.Cards;

        if (cards.Any())
        {
            foreach (var card in cards)
            {
                Cards.Add(card);
            }

            InfoText = $"{Cards.Count} cards loaded from data service";
        }
        else
        {
            InfoText = "No card in collection. Add a few cards to the collection at the beginning.";
        }

        IsLoading = false;
    }

    private async Task DeleteCardsCollectionAsync()
    {
        IsLoading = true;

        // Delete all cards through data service (this will sync with Firestore)
        var deletedCount = 0;
        var cardsToDelete = _flashcardsDataService.Data.Cards.ToList();
        
        foreach (var card in cardsToDelete)
        {
            if (await _flashcardsDataService.DeleteCardAsync(card.Id))
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

        await LoadCardsAsync();
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