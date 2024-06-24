using Bindables.Maui;
using FlashCards.Lib;
using FlashCards.Models;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCardControl : Border
{
    [BindableProperty(typeof(string))] 
    public static readonly BindableProperty PhraseProperty;

    [BindableProperty(typeof(string))] 
    public static readonly BindableProperty TranslationProperty;

    [BindableProperty(typeof(string))] 
    public static readonly BindableProperty ExampleProperty;

    [BindableProperty(typeof(Categories))] 
    public static readonly BindableProperty CategoryProperty;

    [BindableProperty(typeof(LearningProgress))]
    public static readonly BindableProperty LearningProgressProperty;

    [BindableProperty(typeof(bool))] 
    public static readonly BindableProperty FavouriteProperty;

    private bool _frontCardVisible;

    public TwoSidesCardControl()
    {
        InitializeComponent();
        InitializeCurrentSideOfCard();
    }

    private void InitializeCurrentSideOfCard()
    {
        _frontCardVisible = true;
        SetVisibilityProperlySide();
    }

    private async void OnCardTapped(object? sender, TappedEventArgs e)
    {
        _frontCardVisible = !_frontCardVisible;

        await RootOneCardView.RotateYTo(90, 250, Easing.Linear);
        SetVisibilityProperlySide();

        RootOneCardView.RotationY = -90;

        await RootOneCardView.RotateYTo(0, 250, Easing.Linear);
    }

    private void SetVisibilityProperlySide()
    {
        if (_frontCardVisible)
        {
            FrontCard.IsVisible = true;
            SetVisualState(CardStates.Front);
            BackCard.IsVisible = false;
        }
        else
        {
            BackCard.IsVisible = true;
            SetVisualState(CardStates.Back);
            FrontCard.IsVisible = false;
        }
    }

    private void OnFavoriteTapped(object? sender, TappedEventArgs e)
    {
        var newFavourite = !Favourite;
        Favourite = newFavourite;

        //TEST
        Phrase += " FAV";
        Translation += "TRA";
        Example += "EXM";
        //END

        var source = newFavourite
            ? Application.Current.GetResource<string>("icon_heart_marked_SVG")
            : Application.Current.GetResource<string>("icon_heart_unmarked_SVG");

        FavouriteIcon.Source = source;
    }

    private void OnRedCircleTapped(object? sender, TappedEventArgs e)
    {
        if (LearningProgress != LearningProgress.NotStarted)
        {
            LearningProgress = LearningProgress.NotStarted;
            
            // TODO - maybe any converter or VisualState for source
            RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_marked_SVG");
            YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_unmarked_SVG");
            GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_unmarked_SVG");
        }
    }

    private void OnYellowCircleTapped(object? sender, TappedEventArgs e)
    {
        if (LearningProgress != LearningProgress.InProgress)
        {
            LearningProgress = LearningProgress.InProgress;
            
            // TODO - maybe any converter or VisualState for source
            RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_unmarked_SVG");
            YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_marked_SVG");
            GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_unmarked_SVG");
        }
    }

    private void OnGreenCircleTapped(object? sender, TappedEventArgs e)
    {
        if (LearningProgress != LearningProgress.Learned)
        {
            LearningProgress = LearningProgress.Learned;
            
            // TODO - maybe any converter or VisualState for source
            RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_unmarked_SVG");
            YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_unmarked_SVG");
            GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_marked_SVG");
        }
    }

    private void SetVisualState(CardStates state)
        => VisualStateManager.GoToState(this, state.ToString());
}