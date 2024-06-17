namespace FlashCards.Services.Navigation;

public interface INavigationService
{
    Task NavigateToAsync(string page, bool animated = true);
    Task NavigateToAsync<TParam>(string page, bool animated = true);
}

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string page, bool animated = true) =>
        await Shell.Current.GoToAsync(page, true);

    public Task NavigateToAsync<TParam>(string page, bool animated = true)
    {
        throw new NotImplementedException();
    }
}