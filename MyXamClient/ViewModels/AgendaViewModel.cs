using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using System.Collections.ObjectModel;
using MyXamClient.Services.Navigation;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel(INavigationService navigationService) : ObservableObject
{
    [ObservableProperty]
    public static ObservableCollection<AgendaEvent> _agenda = new (AgendaService.Events);

    [ObservableProperty]
    private string _selectedItem;

    [RelayCommand]
    public async Task NavigateToEventPage()
    {
        await navigationService.NavigateAsync("EventPage");
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
        _agenda.Clear();
    }
}
