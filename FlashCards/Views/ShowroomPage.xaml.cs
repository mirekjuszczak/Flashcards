using FlashCards.ViewModels;
using Microsoft.Maui.Controls;

namespace FlashCards.Views;

public partial class ShowroomPage : ContentPage
{
    public ShowroomPage(ShowroomPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}