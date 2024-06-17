using CommunityToolkit.Mvvm.ComponentModel;

namespace FlashCards.ViewModels;

public partial class BaseViewModel: ObservableObject
{
    [ObservableProperty] private string title;
}