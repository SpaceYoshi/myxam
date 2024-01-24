using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}