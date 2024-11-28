using FlashCards.Models;

namespace FlashCards.UI.Controls;

public class VisualStatesCardControl
{
    public const string GroupName = nameof(VisualStatesCardControl);
    
    public static readonly string Front = VisualCardStates.Front.ToString();
    public static readonly string Back = VisualCardStates.Back.ToString();

    public static readonly string NotStarted = VisualCardStates.NotStarted.ToString();
    public static readonly string InProgress = VisualCardStates.InProgress.ToString();
    public static readonly string Learned = VisualCardStates.Learned.ToString();
}