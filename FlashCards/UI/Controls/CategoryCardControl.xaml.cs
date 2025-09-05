using Bindables.Maui;

namespace FlashCards.UI.Controls;

public partial class CategoryCardControl : Border
{
    private static readonly int DefaultIntValue = 0;

    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty CategoryNameProperty;

    [BindableProperty(typeof(int), 
        BindingMode = BindingMode.TwoWay,
        DefaultValueField = nameof(DefaultIntValue))]
    public static readonly BindableProperty CardsCountProperty;

    public CategoryCardControl()
    {
        InitializeComponent();
    }
}