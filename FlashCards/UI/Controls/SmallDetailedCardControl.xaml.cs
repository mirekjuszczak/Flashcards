using Bindables.Maui;
using FlashCards.Models;

namespace FlashCards.UI.Controls;

public partial class SmallDetailedCardControl : Border
{
    public static readonly string DefaultStringValue = string.Empty; 
    
    [BindableProperty(typeof(string), DefaultValueField = nameof(DefaultStringValue))]
    public static readonly BindableProperty CategoryIdProperty;
    
    [BindableProperty(typeof(string), DefaultValueField = nameof(DefaultStringValue))]
    public static readonly BindableProperty PhraseProperty;
    
    [BindableProperty(typeof(string), DefaultValueField = nameof(DefaultStringValue))]
    public static readonly BindableProperty TranslationProperty;
    
    [BindableProperty(typeof(string))]
    public static readonly BindableProperty ExampleProperty;
    
    [BindableProperty(typeof(string), DefaultValueField = nameof(DefaultStringValue))]
    public static readonly BindableProperty CategoryNameProperty;
    
    [BindableProperty(typeof(LearningProgress))]
    public static readonly BindableProperty LearningProgressProperty;
    
    [BindableProperty(typeof(bool))]
    public static readonly BindableProperty FavouriteProperty;
    
    public SmallDetailedCardControl()
    {
        InitializeComponent();
    }
}