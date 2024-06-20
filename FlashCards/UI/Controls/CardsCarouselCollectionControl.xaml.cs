using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CardsCarouselCollectionControl : ContentView
{
    private const double SwipeThreshold = 50;

    [BindableProperty(typeof(List<SingleCard>), OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    public CardsCarouselCollectionControl()
    {
        InitializeComponent();
    }
    
    private static void OnCardsCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        // if (bindable is CardsCarouselCollectionControl element && newvalue is List<SingleCard> newList &&
        //     newvalue != oldvalue)
        // {
        //     element.TwoSidesCard.CurrentCard = newList[element._currentIndex];
        // }
    }
}