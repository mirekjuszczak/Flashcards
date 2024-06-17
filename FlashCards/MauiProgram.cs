using FlashCards.Services.Navigation;
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
		// builder.Services.AddSingleton<MainPage>();
		// builder.Services.AddSingleton<MainPageViewModel>();
		
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<INavigationService, NavigationService>();
	}
}
