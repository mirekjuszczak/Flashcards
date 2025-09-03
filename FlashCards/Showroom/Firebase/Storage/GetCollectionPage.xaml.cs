namespace FlashCards.Showroom.Firebase.Storage;

public partial class GetCollectionPage : ContentPage
{
    public GetCollectionPage(GetCollectionPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}