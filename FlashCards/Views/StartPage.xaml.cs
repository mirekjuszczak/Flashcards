using FlashCards.ViewModels;
using Microsoft.Maui.Controls;

namespace FlashCards.Views;

public partial class StartPage : ContentPage
{
	public StartPage(StartPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

