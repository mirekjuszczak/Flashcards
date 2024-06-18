using FlashCards.Services.Navigation;
using FlashCards.Showroom;

namespace FlashCards.ViewModels;

public class ShowroomPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public ShowroomPageViewModel(INavigationService navigationService)
    {
        Title = "Showroom";
        _navigationService = navigationService;
        OnGoToTwoSidesCardPage = new Command(async () => await RunGoToSidesCardPage());
    }

    public Command OnGoToTwoSidesCardPage { get; }

    private async Task RunGoToSidesCardPage() =>
        await _navigationService.NavigateToAsync(nameof(TwoSidesCardControlPage));
}