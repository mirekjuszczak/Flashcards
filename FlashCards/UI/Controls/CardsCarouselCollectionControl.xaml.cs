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
        Carousel.CurrentItemChanged += CarouselOnCurrentItemChanged;
    }

    private void CarouselOnCurrentItemChanged(object? sender, CurrentItemChangedEventArgs e)
    {
        if (e.PreviousItem != null)
        {
            var previousCardBorder = GetBorderFromDataContext(e.PreviousItem);
            if (previousCardBorder != null)
            {
                Animate(previousCardBorder);
            }
        }

        if (e.CurrentItem != null)
        {
            var currentCardBorder = GetBorderFromDataContext(e.CurrentItem);
            if (currentCardBorder != null)
            {
                Animate(currentCardBorder);
            }
        } 
    }

    private Border? GetBorderFromDataContext(object dataContext)
    {
        var currentElement =  ((List<SingleCard>)dataContext).FirstOrDefault(x => x == dataContext);
        if (currentElement != null)
        {
            return currentElement as Border;
        }
        // foreach (var item in Carousel.ItemsSource)
        // {
        //     var visualElement = Carousel.ItemTemplate.CreateContent() as View;
        //     // if (visualElement?.BindingContext == dataContext)
        //     if ((SingleCard)visualElement?.BindingContext! == dataContext)
        //     {
        //         return visualElement as Border;
        //     }
        // }
        return null;
    }
    
    private async void Animate(Border border)
    {
        await border.RotateYTo(90, 250);
        border.RotationY = -90;
        await border.RotateYTo(0, 250);
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