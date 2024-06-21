using Bindables.Maui;
using FlashCards.Models;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCardControl_Old: Grid
{
    [BindableProperty(typeof(SingleCard))]
    public static readonly BindableProperty CurrentCardProperty;
    
    public TwoSidesCardControl_Old()
    {
        InitializeComponent();
        InitializeProperties();
    }

    private void InitializeProperties()
    {
        FrontCard.IsVisible = true;
        BackCard.IsVisible = false;
    }

    private async void OnFrontCardTapped(object? sender, TappedEventArgs e)
    {
        await FrontCard.RotateYTo(90, 250, Easing.Linear);
        FrontCard.IsVisible = false;
        
        BackCard.RotationY = -90;
        BackCard.IsVisible = true;
        
        await BackCard.RotateYTo(0, 250, Easing.Linear);
    }

    private async void OnBackCardTapped(object? sender, TappedEventArgs e)
    {
        await BackCard.RotateYTo(90, 250, Easing.Linear);
        BackCard.IsVisible = false;
        
        FrontCard.RotationY = -90;
        FrontCard.IsVisible = true;
        
        await FrontCard.RotateYTo(0, 250, Easing.Linear);
    }
}