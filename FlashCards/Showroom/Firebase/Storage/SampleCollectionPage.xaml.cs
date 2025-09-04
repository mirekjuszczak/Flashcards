namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCollectionPage : ContentPage
{
    public SampleCollectionPage(SampleCollectionPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}