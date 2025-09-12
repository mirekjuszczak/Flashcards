using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.DataService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCategoriesCollectionPageViewModel : BaseViewModel
{
    private readonly IFlashcardsDataService _flashcardsDataService;

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _infoText = "Click and get categories from Firestore";

    [ObservableProperty] private string _errorMessage = string.Empty;

    // Direct binding to service's ObservableCollection
    public ObservableCollection<Category> Categories => _flashcardsDataService.Data.Categories;

    public SampleCategoriesCollectionPageViewModel(IFlashcardsDataService flashcardsDataService)
    {
        _flashcardsDataService = flashcardsDataService;
        Title = "Categories collection of Firebase";

        LoadCategoriesCommand = new Command(async () => await LoadCategoriesAsync());
        DeleteCategoriesCollectionCommand = new Command(async () => await DeleteCategoriesCollectionAsync());
        AddSampleCategoryCommand = new Command(async () => await AddSampleCategoryAsync());
    }

    public Command LoadCategoriesCommand { get; }
    public Command DeleteCategoriesCollectionCommand { get; }
    public Command AddSampleCategoryCommand { get; }

    private async Task LoadCategoriesAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;
        InfoText = "Getting categories from Firestore...";

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

        // No need to manually populate - UI will automatically update via ObservableCollection
        var categoriesCount = _flashcardsDataService.Data.Categories.Count;

        InfoText = categoriesCount > 0 
            ? $"{categoriesCount} categories loaded from data service"
            : "No categories in collection. Add a few categories to the collection at the beginning.";

        IsLoading = false;
    }

    private async Task DeleteCategoriesCollectionAsync()
    {
        IsLoading = true;

        // Delete all categories through data service (this will sync with Firestore)
        var deletedCount = 0;
        var categoriesToDelete = _flashcardsDataService.Data.Categories.ToList();
        
        foreach (var category in categoriesToDelete)
        {
            if (category.Id != null && await _flashcardsDataService.DeleteCategoryAsync(category.Id))
            {
                deletedCount++;
            }
        }

        InfoText = deletedCount switch
        {
            > 0 => $"{deletedCount} categories deleted from Firestore",
            0 => "No categories were deleted",
            _ => "Error occurred while deleting categories"
        };

        // No need to call LoadCategoriesAsync - ObservableCollection will auto-update
        IsLoading = false;
    }

    private async Task AddSampleCategoryAsync()
    {
        IsLoading = true;
        InfoText = "Adding a category as an example...";

        var ticksString = DateTime.Now.Ticks.ToString();
        var phraseSufix = ticksString.Substring(ticksString.Length - 3);
        var newCategoryName = $"{RandomTestsValuesMock.GetRandomCategoryName()} {phraseSufix}";

        var addingResult = await _flashcardsDataService.AddCategoryAsync(newCategoryName);
        
        InfoText = addingResult
            ? $"Category '{newCategoryName}' added to Firestore and local data"
            : "Category not added. Validation failed or error occurred.";

        // No need to call LoadCategoriesAsync - ObservableCollection will auto-update
        IsLoading = false;
    }
}