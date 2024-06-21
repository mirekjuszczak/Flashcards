namespace FlashCards.UI.Controls;

public class VisualStatesCardControl
{
    public const string GroupName = nameof(VisualStatesCardControl);
    public static readonly string Front = CardStates.Front.ToString();
    public static readonly string Back = CardStates.Back.ToString();
    public static readonly string FavouriteClicked = CardStates.FavouriteClicked.ToString();
    public static readonly string FavouriteUnClicked = CardStates.FavouriteUnClicked.ToString();
}