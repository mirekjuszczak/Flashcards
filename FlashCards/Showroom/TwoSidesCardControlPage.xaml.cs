using Microsoft.Maui.Controls;

namespace FlashCards.Showroom;

public partial class TwoSidesCardControlPage : ContentPage
{
    public TwoSidesCardControlPage(TwoSidesCardControlPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}