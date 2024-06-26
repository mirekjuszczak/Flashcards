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
        OnGoToCardsSwipeCollectionPage = new Command(async () => await RunGoToOnGoToCardsSwipeCollectionPage());
        OnGoToCardsCarouselCollectionPage = new Command(async () => await RunGoToOnGoToCardsCarouselCollectionPage());
    }

    public Command OnGoToTwoSidesCardPage { get; }

    public Command OnGoToCardsSwipeCollectionPage { get; }

    public Command OnGoToCardsCarouselCollectionPage { get; }

    private async Task RunGoToSidesCardPage() =>
        await _navigationService.NavigateToAsync(nameof(TwoSidesCardControlPage));

    private async Task RunGoToOnGoToCardsSwipeCollectionPage() =>
        await _navigationService.NavigateToAsync(nameof(CardsSwipeCollectionControlPage));

    private async Task RunGoToOnGoToCardsCarouselCollectionPage()=>
        await _navigationService.NavigateToAsync(nameof(CardsCarouselCollectionPage));
}