using MyXamClient.Views;

namespace MyXamClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(EventPage), typeof(EventPage));
        Routing.RegisterRoute(nameof(AgendaPage), typeof(AgendaPage));
    }
}