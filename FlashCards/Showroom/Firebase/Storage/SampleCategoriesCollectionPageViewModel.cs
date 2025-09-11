using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashCards.Services.DatabaseService;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCategoriesCollectionPageViewModel : BaseViewModel
{
    private readonly IDatabaseService _databaseService;

    [ObservableProperty] private ObservableCollection<Category> _categories = new();

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _infoText = "Click and get categories from Firestore";

    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCategoriesCollectionPageViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
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
        Categories.Clear();

        var categories = await _databaseService.GetCategoriesCollection(); 

        if (categories == null)
        {
            ErrorMessage = "Failed to load categories from database";
            InfoText = "Error occurred while loading categories";
        }
        else if (categories.Any())
        {
            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            InfoText = $"{categories.Count} categories loaded Firestore";
        }
        else
        {
            InfoText = "No categories in collection. Add a few categories to the collection at the beginning.";
        }

        IsLoading = false;
    }

    private async Task DeleteCategoriesCollectionAsync()
    {
        IsLoading = true;

        // Delete "categories" collection from Firestore
        var deletingCounter = await _databaseService.DeleteAllCategories();

        InfoText = deletingCounter switch
        {
            > 0 => $"{deletingCounter} categories deleted from Firestore",
            0 => "No category in collection. Add a few categories to the collection at the beginning.",
            -1 => "Something went wrong while deleting categories from Collection",
        };

        if (deletingCounter > 0)
        {
            InfoText = $"{deletingCounter} categories deleted from Firestore";
        }
        else
        {
            InfoText = "No category in collection. Add a few categories to the collection at the beginning.";
        }

        InfoText = "All categories deleted from Collection";

        await LoadCategoriesAsync();

        IsLoading = false;
    }

    private async Task AddSampleCategoryAsync()
    {
        IsLoading = true;
        InfoText = "Adding a category as an example...";

        var ticksString = DateTime.Now.Ticks.ToString();
        var phraseSufix = ticksString.Substring(ticksString.Length - 3);
        var newCategoryName = $"{RandomTestsValuesMock.GetRandomCategoryName()} {phraseSufix}";

        var addingResult = await _databaseService.CreateCategory(newCategoryName);
        
        InfoText = addingResult != null
            ? $"Category '{newCategoryName}' added to Firestore"
            : "Category not added. Validation failed. Name is required.";

        await LoadCategoriesAsync();
    }
}