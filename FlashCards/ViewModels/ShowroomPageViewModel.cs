using FlashCards.Services.Navigation;
using FlashCards.Showroom;
using FlashCards.Showroom.Firebase.Storage;

namespace FlashCards.ViewModels;

public class ShowroomPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public ShowroomPageViewModel(INavigationService navigationService)
    {
        Title = "Showroom";
        _navigationService = navigationService;
        OnGoToTwoSidesCardPage = new Command(async () => await RunGoToSidesCardPage());
        OnGoToCardsSwipeCollectionPage = new Command(async () => await RunGoToCardsSwipeCollectionPage());
        OnGoToCardsCarouselCollectionPage = new Command(async () => await RunGoToCardsCarouselCollectionPage());
        OnGoToCategoryPage = new Command(async () =>await RunGoToCategoryPage());
        OnGoToCategoriesCollectionPage = new Command(async () => await RunGoToCategoriesCollectionPage());
        
        //Firebase
        OnGoToShowCollectionPage = new Command(async () => await RunGoToShowCollectionPage());
    }

    public Command OnGoToTwoSidesCardPage { get; }

    public Command OnGoToCardsSwipeCollectionPage { get; }

    public Command OnGoToCardsCarouselCollectionPage { get; }

    public Command OnGoToCategoryPage { get; }

    public Command OnGoToCategoriesCollectionPage { get; }

    public Command OnGoToShowCollectionPage { get; }

    private async Task RunGoToSidesCardPage() =>
        await _navigationService.NavigateToAsync(nameof(TwoSidesCardControlPage));

    private async Task RunGoToCardsSwipeCollectionPage() =>
        await _navigationService.NavigateToAsync(nameof(CardsSwipeCollectionControlPage));

    private async Task RunGoToCardsCarouselCollectionPage() =>
        await _navigationService.NavigateToAsync(nameof(CardsCarouselCollectionPage));

    private async Task RunGoToCategoryPage()
        => await _navigationService.NavigateToAsync(nameof(CategoryCardPage));

    private async Task RunGoToCategoriesCollectionPage()
        => await _navigationService.NavigateToAsync(nameof(CategoriesCollectionPage));

    private async Task RunGoToShowCollectionPage()
        => await _navigationService.NavigateToAsync(nameof(GetCollectionPage));
}