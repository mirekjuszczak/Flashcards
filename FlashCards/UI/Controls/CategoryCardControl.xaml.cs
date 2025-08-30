using Bindables.Maui;
using Microsoft.Maui.Controls;

namespace FlashCards.UI.Controls;

public partial class CategoryCardControl : Border
{
    [BindableProperty(typeof(string), BindingMode = BindingMode.TwoWay)]
    public static readonly BindableProperty CategoryNameProperty;
    
    public CategoryCardControl()
    {
        InitializeComponent();
    }
}