using FlashCards.Models;
using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CardsCarouselCollectionControl : ContentView
{
    [BindableProperty(typeof(List<SingleCard>), OnPropertyChanged = nameof(OnCardsCollectionPropertyChanged))]
    public static readonly BindableProperty CardsCollectionProperty;

    public CardsCarouselCollectionControl()
    {
        InitializeComponent();
        CarouselCollectionView.CurrentItemChanged += CarouselCollectionViewOnCurrentItemChanged;
    }

    private void CarouselCollectionViewOnCurrentItemChanged(object? sender, CurrentItemChangedEventArgs e)
    {
        var newItem = (SingleCard)e.CurrentItem;
        Console.WriteLine($"MOZU_FLASHCARDS: CarouselCollectionViewOnCurrentItemChanged to:\n" +
                          $"Phrase: {newItem.Phrase}\n" +
                          $"Translation: {newItem.Translation}\n" +
                          $"Example: {newItem.Example}\n" +
                          $"Category: {newItem.Category}\n" +
                          $"LearningProgress: {newItem.LearningProgress}\n" +
                          $"Favourite: {newItem.Favourite}");
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