using FlashCards.Models;
using FlashCards.Services.DatabaseMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CategoriesPageViewModel : BaseViewModel
{
    private readonly IDatabaseServiceMock _databaseServiceMock;
    
    [ObservableAsProperty] public string CategoryName { get; }
    
    [ObservableAsProperty] public List<Category> CategoriesCollection { get; } = new();

    public CategoriesPageViewModel(IDatabaseServiceMock databaseServiceMock)
    {
        _databaseServiceMock = databaseServiceMock;
        Title = "Categories collection";
        CategoryName = "Test Category";

        _ = InitializeCategoriesCollection();
    }
    
    private async Task InitializeCategoriesCollection()
    {
        var collection = await _databaseServiceMock.GetCategoriesCollection();
        CategoriesCollection.AddRange(collection);
    }
}