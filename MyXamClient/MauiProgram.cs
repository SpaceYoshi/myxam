using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyXamClient.Services.Tcp;
using MyXamClient.ViewModels;
using MyXamClient.Views;

namespace MyXamClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<AgendaPage>();
        builder.Services.AddTransient<EventPage>();

        builder.Services.AddSingleton<EventViewModel>();
        builder.Services.AddSingleton<AgendaViewModel>();
        builder.Services.AddSingleton<HomePageViewModel>();

        // builder.Services.AddSingleton<TcpService>();
        // var tcpService = new TcpService();
        // Console.WriteLine("TCP test: " + tcpService);
        TcpService.StartClient();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}