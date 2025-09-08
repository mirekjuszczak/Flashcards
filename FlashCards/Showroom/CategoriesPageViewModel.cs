using FlashCards.Models;
using FlashCards.Services.DatabaseServiceMock;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CategoriesPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public List<Category> CategoriesCollection { get; } = new();

    public CategoriesPageViewModel()
    {
        Title = "Categories collection test";

        InitializeCategoriesCollection();
    }
    
    private void InitializeCategoriesCollection()
    {
        var collection = new DatabaseServiceMock();
        CategoriesCollection.AddRange(collection.CategoriesCollection);
    }
}