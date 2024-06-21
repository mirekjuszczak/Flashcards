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
        SetVisualState(CardStates.Front);
    }

    private void InitializeCurrentSideOfCard()
    {
        RootOneCardView.Content = FrontCard;
        SetVisualState(CardStates.Front);
        _frontCardVisible = true;
    }

    private async void OnCardTapped(object? sender, TappedEventArgs e)
    {
        _frontCardVisible = !_frontCardVisible;
        
        await RootOneCardView.RotateYTo(90, 250, Easing.Linear);
        RootOneCardView.Content = _frontCardVisible ? BackCard : FrontCard;
        SetVisualState(_frontCardVisible ? CardStates.Back : CardStates.Front);

        RootOneCardView.RotationY = -90;
        RootOneCardView.Content = _frontCardVisible ? FrontCard : BackCard;
        SetVisualState(_frontCardVisible ? CardStates.Front : CardStates.Back);

        await RootOneCardView.RotateYTo(0, 250, Easing.Linear);
    }
    
    private void SetVisualState(CardStates state)
        => VisualStateManager.GoToState(this, state.ToString());
}