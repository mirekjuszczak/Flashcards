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
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<IDatabaseServiceMock, DatabaseServiceMock>();
	}
}
