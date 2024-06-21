using Bindables.Maui;
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
    
    private void SetVisualState(CardStates state)
        => VisualStateManager.GoToState(this, state.ToString());
}