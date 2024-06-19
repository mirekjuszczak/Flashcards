namespace FlashCards.Showroom;

public partial class CardsCollectionControlPage : ContentPage
{
    public CardsCollectionControlPage(CardsCollectionControlPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}