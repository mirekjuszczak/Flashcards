using Microsoft.Maui.Controls.Shapes;

namespace FlashCards.UI.Controls;

public partial class TwoSidesCard: VerticalStackLayout
{
    public TwoSidesCard()
    {
        InitializeComponent();
    }
    
    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    
        switch (propertyName)
        {
            case nameof(Width):
            case nameof(Height):
                UpdateClip();
                break;
        }
    }
    
    private void UpdateClip()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }
    
        var radius = Height / 10;
        Clip = new RoundRectangleGeometry(new CornerRadius(radius), new Rect(0, 0, Width, Height));
    }
}