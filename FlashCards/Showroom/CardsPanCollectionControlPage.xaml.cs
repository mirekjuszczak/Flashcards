namespace FlashCards.Showroom;

public partial class CardsPanCollectionControlPage : ContentPage
{
    public CardsPanCollectionControlPage(CardsCollectionControlPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}