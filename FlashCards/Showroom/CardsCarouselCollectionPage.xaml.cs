using Microsoft.Maui.Controls;

namespace FlashCards.Showroom;

public partial class CardsCarouselCollectionPage : ContentPage
{
    public CardsCarouselCollectionPage(CardsCollectionControlPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}