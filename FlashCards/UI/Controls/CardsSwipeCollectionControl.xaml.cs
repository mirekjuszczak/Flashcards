using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Models;
using Bindables.Maui;
using FlashCards.Models.Dto;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlashCards.UI.Controls;

public partial class CardsSwipeCollectionControl : ContentView
{
    private const int TimeOfAnimation = 150;
    private const float OpacityWhenMoving = 0.5f;

    [BindableProperty(typeof(List<SingleCardDto>), 
        BindingMode = BindingMode.TwoWay,
        OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    private int _currentIndex;

    public CardsSwipeCollectionControl()
    {
        _currentIndex = 0;
        InitializeComponent();
    }

    private async void OnSwipeNext(object? sender, SwipedEventArgs swipedEventArgs)
    {
        if (CardsCollection != null)
        {
            await RunAnimationOnChangingCard(1, TimeOfAnimation);

            UpdateCardInCollection();
            _currentIndex = _currentIndex < CardsCollection.Count - 1 ? _currentIndex + 1 : 0;
            UpdateDisplayedCard();

            await FinishAnimationOnChangingCard(TimeOfAnimation);
        }
    }

    private async void OnSwipePrevious(object? sender, SwipedEventArgs swipedEventArgs)
    {
        if (CardsCollection != null)
        {
            await RunAnimationOnChangingCard(-1, TimeOfAnimation);

            UpdateCardInCollection();
            _currentIndex = _currentIndex > 0 ? _currentIndex - 1 : CardsCollection.Count - 1;
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
            GetNextCard(item);
        }
    }

    private static void OnCardsCollectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is CardsSwipeCollectionControl element && newvalue is List<SingleCard> newList &&
            newvalue != oldvalue)
        {
            element.UpdateDisplayedCard();
        }
    }

    private void GetNextCard(SingleCardDto item)
    {
        TwoSidesCard.Phrase = item.Phrase;
        TwoSidesCard.Translation = item.Translation;
        TwoSidesCard.Example = item.Example;
        TwoSidesCard.CategoryName = item.CategoryName;
        TwoSidesCard.LearningProgress = item.LearningProgress;
        TwoSidesCard.Favourite = item.Favourite;
    }

    private void UpdateCardInCollection()
    {
        if (CardsCollection != null)
        {
            var currentCard = CardsCollection[_currentIndex];
            var cardInCollection = CardsCollection?.FirstOrDefault(card => card.Phrase == currentCard.Phrase);
    
            if (cardInCollection != null)
            {
                cardInCollection.Phrase = TwoSidesCard.Phrase ?? string.Empty;
                cardInCollection.Translation = TwoSidesCard.Translation  ?? string.Empty;
                cardInCollection.Example = TwoSidesCard.Example  ?? string.Empty;
                cardInCollection.CategoryName = TwoSidesCard.CategoryName;
                cardInCollection.LearningProgress = TwoSidesCard.LearningProgress;
                cardInCollection.Favourite = TwoSidesCard.Favourite;
            }
        }
    }
}