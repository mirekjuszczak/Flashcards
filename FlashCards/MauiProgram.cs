using FlashCards.Services.DatabaseMock;
using FlashCards.Services.Navigation;
using FlashCards.Showroom;
using FlashCards.ViewModels;
using FlashCards.Views;
using Microsoft.Extensions.Logging;

namespace FlashCards;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Font-Regular.ttf");
				fonts.AddFont("Font-SemiBold.ttf");
				fonts.AddFont("Font-Bold.ttf");
			});
		
		RegisterViewModelsAndPages(builder);
		RegisterServices(builder);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	
	private static void RegisterViewModelsAndPages(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<StartPage>();
		builder.Services.AddSingleton<StartPageViewModel>();
		
		builder.Services.AddSingleton<ShowroomPage>();
		builder.Services.AddSingleton<ShowroomPageViewModel>();
		
		builder.Services.AddSingleton<TwoSidesCardControlPage>();
		builder.Services.AddSingleton<TwoSidesCardControlPageViewModel>();
		
		builder.Services.AddSingleton<CardsSwipeCollectionControlPage>();
		builder.Services.AddSingleton<CardsCarouselCollectionPage>();
		builder.Services.AddSingleton<CardsCollectionControlPageViewModel>();
		builder.Services.AddSingleton<CardsCollection3Page>();
		builder.Services.AddSingleton<CardsCollection3ViewModel>();
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<IDatabaseServiceMock, DatabaseServiceMock>();
	}
}
