namespace FlashCards.Showroom;

public partial class CardsSwipeCollectionControlPage : ContentPage
{
    public CardsSwipeCollectionControlPage(CardsCollectionControlPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}