namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCardsCollectionPage : ContentPage
{
    public SampleCardsCollectionPage(SampleCardsCollectionPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}