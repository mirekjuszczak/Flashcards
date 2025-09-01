using Microsoft.Maui.Controls;

namespace FlashCards.Showroom;

public partial class CardsCollection3Page : ContentPage
{
    public CardsCollection3Page(CardsCollection3ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}