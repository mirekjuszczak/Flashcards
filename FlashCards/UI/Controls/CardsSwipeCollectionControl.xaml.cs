using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CardsSwipeCollectionControl : ContentView
{
    [BindableProperty(typeof(List<SingleCard>), OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;
    
    private int _currentIndex = 0;
    
    public CardsSwipeCollectionControl()
    {
        InitializeComponent();
    }

    private void OnSwipeNext(object? sender, EventArgs e)
    {
        if (CardsCollection != null && _currentIndex < CardsCollection.Count - 1)
        {
            _currentIndex++;
            UpdateDisplayedCard();
        }
    }

    private void OnSwipePrevious(object? sender, EventArgs e)
    {
        if (CardsCollection != null && _currentIndex > 0)
        {
            _currentIndex--;
            UpdateDisplayedCard();
        }
    }

    private void UpdateDisplayedCard()
    {
        if (CardsCollection != null && CardsCollection.Any())
        {
            // TwoSidesCard.CurrentCard = CardsCollection[_currentIndex];
        }
    }
    
    private static void OnCardsCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is CardsSwipeCollectionControl element && newvalue is List<SingleCard> newList && newvalue != oldvalue)
        {
            // element.TwoSidesCard.CurrentCard = newList[element._currentIndex];
        }
    }
}