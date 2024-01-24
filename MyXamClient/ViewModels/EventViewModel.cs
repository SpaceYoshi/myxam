using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class EventViewModel : ObservableObject
{
    [ObservableProperty]
    int _id;

    [ObservableProperty]
    string _name;

    [ObservableProperty]
    string _description;

    [ObservableProperty]
    DateTimeOffset _startTime;

    [ObservableProperty]
    DateTimeOffset _endTime;

    [RelayCommand]
    private static async Task GoBack()
    {
        await AgendaViewModel.UpdateAgenda();
        await Shell.Current.GoToAsync("AgendaPage");
    }

    [RelayCommand]
    private void AddEvent()
    {
        AgendaService.Events.Add(CreateEvent());
    }

    private AgendaEvent CreateEvent()
    {
        var newEvent = new AgendaEvent
        (
            id: Guid.NewGuid(),
            agendaId: Guid.NewGuid(),
            name: Name,
            startTime: StartTime,
            endTime: EndTime
        );

        return newEvent;
    }
}