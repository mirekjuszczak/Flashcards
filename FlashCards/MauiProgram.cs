using FlashCards.Services.DatabaseService;
using FlashCards.Services.FirebaseService;
using FlashCards.Services.Navigation;
using FlashCards.Showroom;
using FlashCards.ViewModels;
using FlashCards.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FlashCards.Services.DatabaseServiceMock;
using FlashCards.Showroom.Firebase.Storage;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
using Plugin.Firebase.Storage;

#if IOS
using Plugin.Firebase.Core.Platforms.iOS;
#elif ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#endif

namespace FlashCards;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("Font-Regular.ttf");
            fonts.AddFont("Font-SemiBold.ttf");
            fonts.AddFont("Font-Bold.ttf");
        }).UseMauiCommunityToolkit();
        RegisterViewModelsAndPages(builder);
        RegisterServices(builder);
        RegisterFirebaseServices(builder);
        
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
        
        builder.Services.AddSingleton<CategoryCardPage>();
        builder.Services.AddSingleton<CategoriesCollectionPage>();
        builder.Services.AddSingleton<CategoriesPageViewModel>();
        
        builder.Services.AddSingleton<AuthorisationPage>();
        builder.Services.AddSingleton<AuthorisationPageViewModel>();
        
        // Firebase
        builder.Services.AddSingleton<SampleCardsCollectionPage>();
        builder.Services.AddSingleton<SampleCardsCollectionPageViewModel>();
    }

    private static void RegisterServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDatabaseService, FirebaseDatabaseService>();
        builder.Services.AddSingleton<IUserService, FirebaseAuthService>();
    }
    
    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((_,__) => {
                CrossFirebase.Initialize();
                return false;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, _) =>
                CrossFirebase.Initialize(activity)));
#endif
        });
        
        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
        builder.Services.AddSingleton(_ => CrossFirebaseStorage.Current);
        builder.Services.AddSingleton(_ => CrossFirebaseFirestore.Current);
        return builder;
    }
}