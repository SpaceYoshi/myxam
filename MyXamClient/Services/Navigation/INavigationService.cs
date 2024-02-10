namespace MyXamClient.Services.Navigation;

public interface INavigationService
{
    Task NavigateAsync(string route);
    Task NavigateAsync(string route, Dictionary<string, object> parameters);
}