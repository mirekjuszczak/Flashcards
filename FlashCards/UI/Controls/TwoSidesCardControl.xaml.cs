using Bindables.Maui;
using FlashCards.Lib;
using FlashCards.Models;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCardControl : Border
{
    [BindableProperty(typeof(string))] public static readonly BindableProperty PhraseProperty;

    [BindableProperty(typeof(string))] public static readonly BindableProperty TranslationProperty;

    [BindableProperty(typeof(string))] public static readonly BindableProperty ExampleProperty;

    [BindableProperty(typeof(Categories))] public static readonly BindableProperty CategoryProperty;

    [BindableProperty(typeof(LearningProgress))]
    public static readonly BindableProperty LearningProgressProperty;

    [BindableProperty(typeof(bool))] public static readonly BindableProperty FavouriteProperty;

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
        //tylko test potem odczytac poprawny stan  usatwic na starcie
        // SetVisualState(VisualCardStates.NotStarted);

        if (_frontCardVisible)
        {
            FrontCard.IsVisible = true;
            SetVisualState(VisualCardStates.Front);
            BackCard.IsVisible = false;
        }
        else
        {
            BackCard.IsVisible = true;
            SetVisualState(VisualCardStates.Back);
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

            SetVisualState(RedCircle, VisualCardStates.NotStarted);
            SetVisualState(YellowCircle, VisualCardStates.NotStarted);
            SetVisualState(GreenCircle, VisualCardStates.NotStarted);
        }
    }

    private void OnYellowCircleTapped(object? sender, TappedEventArgs e)
    {
        if (LearningProgress != LearningProgress.InProgress)
        {
            LearningProgress = LearningProgress.InProgress;

            SetVisualState(RedCircle, VisualCardStates.InProgress);
            SetVisualState(YellowCircle, VisualCardStates.InProgress);
            SetVisualState(GreenCircle, VisualCardStates.InProgress);
        }
    }

    private void OnGreenCircleTapped(object? sender, TappedEventArgs e)
    {
        if (LearningProgress != LearningProgress.Learned)
        {
            LearningProgress = LearningProgress.Learned;

            SetVisualState(RedCircle, VisualCardStates.Learned);
            SetVisualState(YellowCircle, VisualCardStates.Learned);
            SetVisualState(GreenCircle, VisualCardStates.Learned);
        }
    }

    private void SetVisualState(VisualCardStates state)
        => VisualStateManager.GoToState(this, state.ToString());

    private void SetVisualState(VisualElement element, VisualCardStates state)
        => VisualStateManager.GoToState(element, state.ToString());
}