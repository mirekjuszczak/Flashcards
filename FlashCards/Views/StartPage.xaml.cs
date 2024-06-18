using FlashCards.ViewModels;

namespace FlashCards.Views;

public partial class StartPage : ContentPage
{
	int count = 0;

	public StartPage(StartPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

