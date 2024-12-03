using FlashCards.Models;
using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom;

public class CategoriesPageViewModel : BaseViewModel
{
    [ObservableAsProperty] public string CategoryName { get; }

    public CategoriesPageViewModel()
    {
        Title = "Categories";
        CategoryName = "Test Category";
    }
}