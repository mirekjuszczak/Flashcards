namespace FlashCards.Showroom;

public partial class CategoryCardPage : ContentPage
{
    public CategoryCardPage(CategoriesPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}