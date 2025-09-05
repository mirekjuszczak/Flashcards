using System;
using Bindables.Maui;
using FlashCards.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCardControl : Border
{
    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty PhraseProperty;

    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty TranslationProperty;

    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty ExampleProperty;

    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty CategoryIdProperty;
    
    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty CategoryNameProperty;

    [BindableProperty(typeof(LearningProgress), 
        BindingMode = BindingMode.TwoWay,
        OnPropertyChanged = nameof(OnLearningProgressPropertyChanged))]
    public static readonly BindableProperty LearningProgressProperty;

    [BindableProperty(typeof(bool), 
        BindingMode = BindingMode.TwoWay,
        OnPropertyChanged = nameof(OnFavouritePropertyChanged))
    ]
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
        Translation += " TRA";
        Example += " EXM";
        //END

        if (Favourite)
        {
            SetVisualState(FavouriteIcon, VisualCardStates.FavouriteEnabled);
        }
        else
        {
            SetVisualState(FavouriteIcon, VisualCardStates.FavouriteDisabled);
        }
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
    
    private static void OnFavouritePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is TwoSidesCardControl element && newvalue is bool && newvalue != oldvalue)
        {
            element.UpdateCurrentFavouriteVisualState();
        }
    }
    
    private static void OnLearningProgressPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is TwoSidesCardControl element && newvalue is LearningProgress && newvalue != oldvalue)
        {
            element.UpdateCurrentLearningProgress();
        }
    }
    
    private void UpdateCurrentLearningProgress()
    {
        var progress = LearningProgress switch
        {
            LearningProgress.NotStarted => VisualCardStates.NotStarted,
            LearningProgress.InProgress => VisualCardStates.InProgress,
            LearningProgress.Learned => VisualCardStates.Learned,
            _ => throw new ArgumentOutOfRangeException($"Argument {LearningProgress} not supported")
        };

        SetVisualState(RedCircle, progress);
        SetVisualState(YellowCircle, progress);
        SetVisualState(GreenCircle, progress);
    }

    private void UpdateCurrentFavouriteVisualState()
    {
        var state = Favourite switch
        {
            true => VisualCardStates.FavouriteEnabled,
            false => VisualCardStates.FavouriteDisabled
        };

        SetVisualState(FavouriteIcon, state);
    }

    private void SetVisualState(VisualCardStates state)
        => VisualStateManager.GoToState(this, state.ToString());

    private void SetVisualState(VisualElement element, VisualCardStates state)
        => VisualStateManager.GoToState(element, state.ToString());
}