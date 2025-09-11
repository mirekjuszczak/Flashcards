namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCategoriesCollectionPage : ContentPage
{
    public SampleCategoriesCollectionPage(SampleCategoriesCollectionPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}