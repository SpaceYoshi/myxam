using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Models;
using MyXamClient.Services;
using MyXamClient.Views;
using System.Collections.ObjectModel;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    [ObservableProperty]
    private static ObservableCollection<AgendaEvent> agenda = new (AgendaService.getEvents());

    [ObservableProperty]
    private string selectedItem;

    [RelayCommand]
    public async Task NavigateToEventPage()
    {
        await Shell.Current.GoToAsync("EventPage");
    }

    public static async Task UpdateAgenda()
    {
        var items = await Task.Run(() => AgendaService.getEvents());

        foreach (var item in items) 
        {
            if (!agenda.Contains(item))
            {
                agenda.Add(item);
            }
        }
    }
}
