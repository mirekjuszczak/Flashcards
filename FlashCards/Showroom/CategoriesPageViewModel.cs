using FlashCards.Models;
using FlashCards.Services.DatabaseService;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CategoriesPageViewModel : BaseViewModel
{
    private readonly IDatabaseService _databaseService;
    
    [ObservableAsProperty] public string CategoryName { get; }
    
    [ObservableAsProperty] public List<Category> CategoriesCollection { get; } = new();

    public CategoriesPageViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
        Title = "Categories collection";
        CategoryName = "Test Category";

        _ = InitializeCategoriesCollection();
    }
    
    private async Task InitializeCategoriesCollection()
    {
        var collection = await _databaseService.GetCategoriesCollection();
        CategoriesCollection.AddRange(collection);
    }
}