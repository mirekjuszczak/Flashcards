using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Models;
using Bindables.Maui;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlashCards.UI.Controls;

public partial class CardsCollectionControl3 : ContentView
{
    private const int TimeOfAnimation = 150;
    private const float OpacityWhenMoving = 0.5f;

    [BindableProperty(typeof(List<SingleCard>), 
        BindingMode = BindingMode.TwoWay,
        OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    private int _currentIndex;

    public CardsCollectionControl3()
    {
        _currentIndex = 0;
        InitializeComponent();
    }

    private async void OnSwipeNext(object? sender, SwipedEventArgs swipedEventArgs)
    {
        if (CardsCollection != null)
        {
            await RunAnimationOnChangingCard(1, TimeOfAnimation);

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
            TwoSidesCardCollection.TranslateTo(direction * TwoSidesCardCollection.Width, 0, duration, Easing.CubicInOut),
            TwoSidesCardCollection.FadeTo(OpacityWhenMoving, (uint)time),
            TwoSidesCardCollection.ScaleTo(0.8, duration, Easing.CubicInOut)
        );
    }

    private async Task FinishAnimationOnChangingCard(int time)
    {
        await Task.Delay(time);
        TwoSidesCardCollection.TranslationX = 0;
        await TwoSidesCardCollection.ScaleTo(1, (uint)time);
        await TwoSidesCardCollection.FadeTo(1, (uint)time);
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
        if (bindable is CardsCollectionControl3 element && newvalue is List<SingleCard> newList &&
            newvalue != oldvalue)
        {
            // if (element.CardsCollection != null)
            // {
            //     // element.CardsCollection[element._currentIndex] = newList[element._currentIndex];
            //     element.CardsCollection = newList;
            // }
            
            element.UpdateDisplayedCard();
        }
    }

    private void UpdateCard(SingleCard item)
    {
        // TwoSidesCard.Phrase = item.Phrase;
        // TwoSidesCard.Translation = item.Translation;
        // TwoSidesCard.Example = item.Example;
        // TwoSidesCard.Category = item.Category;
        // TwoSidesCard.LearningProgress = item.LearningProgress;
        // TwoSidesCard.Favourite = item.Favourite;
        
        UpdateCardInCollection(item);
    }

    private void UpdateCardInCollection(SingleCard item)
    {
        var cardInCollection = CardsCollection?.FirstOrDefault(card => card.Phrase == item.Phrase);
    
        if (cardInCollection != null)
        {
            cardInCollection.Phrase = item.Phrase;
            cardInCollection.Translation = item.Translation;
            cardInCollection.Example = item.Example;
            cardInCollection.CategoryName = item.CategoryName;
            cardInCollection.LearningProgress = item.LearningProgress;
            cardInCollection.Favourite = item.Favourite;
        }
    }
}