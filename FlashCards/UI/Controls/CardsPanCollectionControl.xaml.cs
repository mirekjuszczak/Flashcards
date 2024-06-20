using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CardsPanCollectionControl : ContentView
{
    private const double SwipeThreshold = 50;

    [BindableProperty(typeof(List<SingleCard>), OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    private int _currentIndex = 0;
    private double _startPosisionX;
    // private double _startPositionY;

    public CardsPanCollectionControl()
    {
        InitializeComponent();
    }

    private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                // Handle the start values in needed
                _startPosisionX = e.TotalX;
                // _startPositionY = e.TotalY;
                break;

            case GestureStatus.Running:
                // Handle the pan gesture
                double boundsX = RootCardCollectionView.Width;
                TwoSidesCard.TranslationX = Math.Clamp(e.TotalX, -boundsX / 2, boundsX / 2);
                break;

            case GestureStatus.Canceled:
            case GestureStatus.Completed:
                if (TwoSidesCard.TranslationX < -SwipeThreshold)
                {
                    // Swiped left
                    OnSwipeNext(sender, EventArgs.Empty);
                    TwoSidesCard.TranslationX = _startPosisionX;
                    // TwoSidesCard.TranslationY = _startPositionY;
                }
                else if (TwoSidesCard.TranslationX > SwipeThreshold)
                {
                    // Swiped right
                    OnSwipePrevious(sender, EventArgs.Empty);
                }

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
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
            TwoSidesCard.CurrentCard = CardsCollection[_currentIndex];
        }
    }

    private static void OnCardsCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is CardsPanCollectionControl element && newvalue is List<SingleCard> newList &&
            newvalue != oldvalue)
        {
            element.TwoSidesCard.CurrentCard = newList[element._currentIndex];
        }
    }
}