using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CardsSwipeCollectionControl : ContentView
{
    private const int TimeOfAnimation = 150;
    private const float OpacityWhenMoving = 0.5f;

    [BindableProperty(typeof(List<SingleCard>), OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    private int _currentIndex = 0;

    public CardsSwipeCollectionControl()
    {
        InitializeComponent();
    }

    private async void OnSwipeNext(object? sender, SwipedEventArgs swipedEventArgs)
    {
        if (CardsCollection != null && _currentIndex < CardsCollection.Count - 1)
        {
            await RunAnimationOnChangingCard(1, TimeOfAnimation);

            _currentIndex++;
            UpdateDisplayedCard();

            await FinishAnimationOnChangingCard(TimeOfAnimation);
        }
    }

    private async void OnSwipePrevious(object? sender, SwipedEventArgs swipedEventArgs)
    {
        if (CardsCollection != null && _currentIndex > 0)
        {
            await RunAnimationOnChangingCard(-1, TimeOfAnimation);

            _currentIndex--;
            UpdateDisplayedCard();

            await FinishAnimationOnChangingCard(TimeOfAnimation);
        }
    }

    private async Task RunAnimationOnChangingCard(int direction, int time)
    {
        var duration = (uint)time * 2;

        // direction == 1 Next; direction == -1 Previous
        await Task.WhenAll(
            TwoSidesCard.TranslateTo(direction * TwoSidesCard.Width, 0, duration, Easing.CubicInOut),
            TwoSidesCard.FadeTo(OpacityWhenMoving, (uint)time),
            TwoSidesCard.ScaleTo(0.8, duration, Easing.CubicInOut)
        );
    }

    private async Task FinishAnimationOnChangingCard(int time)
    {
        await Task.Delay(time);
        TwoSidesCard.TranslationX = 0;
        await TwoSidesCard.ScaleTo(1, (uint)time);
        await TwoSidesCard.FadeTo(1, (uint)time);
    }

    private void UpdateDisplayedCard()
    {
        if (CardsCollection != null && CardsCollection.Any())
        {
            var item = CardsCollection[_currentIndex];
            UpdateCard(item);
        }
    }

    private static void OnCardsCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is CardsSwipeCollectionControl element && newvalue is List<SingleCard> newList &&
            newvalue != oldvalue)
        {
            // element.TwoSidesCard.CurrentCard = newList[element._currentIndex];
        }
    }

    private void UpdateCard(SingleCard item)
    {
        TwoSidesCard.Phrase = item.Phrase;
        TwoSidesCard.Translation = item.Translation;
        TwoSidesCard.Example = item.Example;
        TwoSidesCard.Category = item.Category;
        TwoSidesCard.LearningProgress = item.LearningProgress;
        TwoSidesCard.Favourite = item.Favourite;
    }
}