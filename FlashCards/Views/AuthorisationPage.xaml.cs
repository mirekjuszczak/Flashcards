using FlashCards.ViewModels;

namespace FlashCards.Views;

public partial class AuthorisationPage : ContentPage
{
    public AuthorisationPage(AuthorisationPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}