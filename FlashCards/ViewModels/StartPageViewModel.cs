using FlashCards.Services.Navigation;
using FlashCards.Views;

namespace FlashCards.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    public StartPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        Title = "Your Flash Cards";
        OnGoToShowroomCommand = new Command(async () => await RunGoToShowroom());
    }
    
    public Command OnGoToShowroomCommand { get; }
    
    private async Task RunGoToShowroom() => 
        await _navigationService.NavigateToAsync(nameof(ShowroomPage));
}