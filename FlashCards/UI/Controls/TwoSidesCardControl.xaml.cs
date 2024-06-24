using Bindables.Maui;
using FlashCards.Lib;
using FlashCards.Models;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCardControl: Border
{
    [BindableProperty(typeof(SingleCard))]
    public static readonly BindableProperty CurrentCardProperty;

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
        if (CurrentCard != null)
        {
            var newFavourite = !CurrentCard.Favourite;
            CurrentCard.Favourite = newFavourite;
            
            //TEST
            CurrentCard.Phrase += " FAVOURITE";
            //END

            var source = newFavourite
                ? Application.Current.GetResource<string>("icon_heart_marked_SVG")
                : Application.Current.GetResource<string>("icon_heart_unmarked_SVG");

            FavouriteIcon.Source = source;
        }
    }

    private void OnRedCircleTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentCard != null)
        {
            if (CurrentCard.LearningProgress != LearningProgress.NotStarted)
            {
                CurrentCard.LearningProgress = LearningProgress.NotStarted;
                RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_marked_SVG");
                YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_unmarked_SVG");
                GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_unmarked_SVG");
            }
        }
    }

    private void OnYellowCircleTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentCard != null)
        {
            if (CurrentCard.LearningProgress != LearningProgress.InProgress)
            {
                CurrentCard.LearningProgress = LearningProgress.InProgress;
                RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_unmarked_SVG");
                YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_marked_SVG");
                GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_unmarked_SVG");
            }
        }
    }

    private void OnGreenCircleTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentCard != null)
        {
            if (CurrentCard.LearningProgress != LearningProgress.Learned)
            {
                CurrentCard.LearningProgress = LearningProgress.Learned;
                RedCircle.Source = Application.Current.GetResource<string>("icon_circle_red_unmarked_SVG");
                YellowCircle.Source = Application.Current.GetResource<string>("icon_circle_yellow_unmarked_SVG");
                GreenCircle.Source = Application.Current.GetResource<string>("icon_circle_green_marked_SVG");
            }
        }
    }

    private void SetVisualState(CardStates state)
        => VisualStateManager.GoToState(this, state.ToString());
}