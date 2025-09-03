using FlashCards.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace FlashCards.Showroom.Firebase.Storage;

public class GetCollectionPageViewModel : BaseViewModel
{
    public GetCollectionPageViewModel()
    {
        
    }
    
    [ObservableAsProperty] public string InfoText { get; } = "Collection not implemented yet";
}