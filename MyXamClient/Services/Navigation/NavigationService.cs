namespace MyXamClient.Services.Navigation;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task NavigateAsync(string route, Dictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(route, parameters);
    }
}