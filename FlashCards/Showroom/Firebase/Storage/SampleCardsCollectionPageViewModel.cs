using FlashCards.ViewModels;
using FlashCards.Models;
using FlashCards.Models.Dto;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.FlashcardsDataService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCardsCollectionPageViewModel : BaseViewModel
{
    private readonly IFlashcardsDataService _flashcardsDataService;

    // Changed to use SingleCardDto instead of SingleCard
    public ObservableCollection<SingleCardDto> Cards { get; }
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _infoText = "Click and get cards from Firestore";
    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCardsCollectionPageViewModel(IFlashcardsDataService flashcardsDataService)
    {
        _flashcardsDataService = flashcardsDataService;
        Title = "Cards collection of Firebase";
        
        // Initialize Cards collection using DTO method
        Cards = new ObservableCollection<SingleCardDto>();

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
        
        // Update Cards collection with DTOs
        var cardsDto = _flashcardsDataService.GetCardsAsDto();
        Cards.Clear();
        foreach (var card in cardsDto)
        {
            Cards.Add(card);
        }
        
        var cardsCount = Cards.Count;

        InfoText = cardsCount > 0 
            ? $"{cardsCount} cards loaded from data service"
            : "No card in collection. Add a few cards to the collection at the beginning.";

        IsLoading = false;
    }

    private async Task DeleteCardsCollectionAsync()
    {
        IsLoading = true;
        
        var deletedCount = await _flashcardsDataService.DeleteAllCardsAsync();

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

        var categoryId = RandomlyChooseCategoryId();

        var sampleCard = new SingleCard
        {
            Phrase = $"Hello {phraseSufix}",
            Translation = $"Czesc {phraseSufix}",
            Example = "Hello, how are you?",
            CategoryId = categoryId,
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

    private string RandomlyChooseCategoryId()
    {
        string categoryId = string.Empty;
        var random = new Random();
        var categories = _flashcardsDataService.Data.Categories.ToList();
        
        if (categories.Any() && RandomTestsValuesMock.GetRandomNumber(0, 3) != 0) // 2/3 chance to use existing category
        {
            var randomCategory = categories[random.Next(categories.Count)];
            categoryId = randomCategory.Id!;
        }
        else if (RandomTestsValuesMock.GetRandomNumber(0, 2) == 0) // 1/2 chance for non-existing when no existing chosen
        { 
            InfoText = "Adding card with undefined category";
        }

        return categoryId;
    }
}