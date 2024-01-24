using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using System.Collections.ObjectModel;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    [ObservableProperty]
    private static ObservableCollection<AgendaEvent> _agenda = new (AgendaService.Events);

    [ObservableProperty]
    private string _selectedItem;

    [RelayCommand]
    private static async Task NavigateToEventPage()
    {
        await Shell.Current.GoToAsync("EventPage");
    }

    public static async Task UpdateAgenda()
    {
        var items = await Task.Run(() => AgendaService.Events);

        foreach (var item in items) 
        {
            if (!_agenda.Contains(item))
            {
                _agenda.Add(item);
            }
        }
    }

    public static void ClearAgenda()
    {
        agenda.Clear();
    }
}
