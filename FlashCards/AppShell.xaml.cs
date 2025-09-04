using FlashCards.Showroom;
using FlashCards.Showroom.Firebase.Storage;
using FlashCards.Views;

namespace FlashCards;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		AddRegisteringRoute();
	}
	
	private static void AddRegisteringRoute()
	{
		Routing.RegisterRoute(nameof(ShowroomPage), typeof(ShowroomPage));
		Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
		Routing.RegisterRoute(nameof(TwoSidesCardControlPage), typeof(TwoSidesCardControlPage));
		Routing.RegisterRoute(nameof(CardsSwipeCollectionControlPage), typeof(CardsSwipeCollectionControlPage));
		Routing.RegisterRoute(nameof(CardsCarouselCollectionPage), typeof(CardsCarouselCollectionPage));
		Routing.RegisterRoute(nameof(CategoryCardPage), typeof(CategoryCardPage));
		Routing.RegisterRoute(nameof(CategoriesCollectionPage), typeof(CategoriesCollectionPage));
		
		// Firebase
		Routing.RegisterRoute(nameof(SampleCollectionPage), typeof(SampleCollectionPage));
	}
}
