using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCollectionPageViewModel : BaseViewModel
{
    private readonly IDatabaseService _databaseService;

    [ObservableProperty] private ObservableCollection<SingleCard> _cards = new();

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _infoText = "Click and get cards from Firestore";

    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCollectionPageViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
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
        Cards.Clear();

        var cards = await _databaseService.GetCardsCollection();

        if (cards == null)
        {
            ErrorMessage = "Failed to load cards from database";
            InfoText = "Error occurred while loading cards";
        }
        else if (cards.Any())
        {
            foreach (var card in cards)
            {
                Cards.Add(card);
            }

            InfoText = $"{Cards.Count} cards loaded Firestore";
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

        // Delete "cards" collection from Firestore
        var deletingCounter = await _databaseService.DeleteAllCards();

        InfoText = deletingCounter switch
        {
            > 0 => $"{deletingCounter} cards deleted from Firestore",
            0 => "No card in collection. Add a few cards to the collection at the beginning.",
            -1 => "Something went wrong while deleting cards from Collection",
        };

        if (deletingCounter > 0)
        {
            InfoText = $"{deletingCounter} cards deleted from Firestore";
        }
        else
        {
            InfoText = "No card in collection. Add a few cards to the collection at the beginning.";
        }

        InfoText = "All cards deleted from Collection";

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
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = "noname",
            Favourite = false,
            LearningProgress = LearningProgress.NotStarted
        };

        var addingResult = await _databaseService.CreateCard(sampleCard);

        InfoText = addingResult switch
        {
            true => "Card added to Firestore",
            false => "Card not adde. Validation failed. Phrase and Translation are required.",
            null => "Card not added, Exception invoked.",
        };

        await LoadCardsAsync();
    }
}