using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CategoriesCollectionControl : ContentView
{
    [BindableProperty(typeof(List<Category>), OnPropertyChanged = nameof(OnCategoryCollectionPropertyChanged))]
    public static readonly BindableProperty CategoryCollectionProperty;
    
    public CategoriesCollectionControl()
    {
        InitializeComponent();
    }
    
    private static void OnCategoryCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is CategoriesCollectionControl element && newvalue is List<Category> newList &&
            newvalue != oldvalue)
        {
            
        }
    }
}