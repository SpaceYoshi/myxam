using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using MyXamClient.Services.Tcp;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class EventViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _location;

    [ObservableProperty]
    private DateTimeOffset _startTime;

    [ObservableProperty]
    private DateTimeOffset _endTime;

    [RelayCommand]
    private static async Task GoBack()
    {
        await AgendaViewModel.UpdateAgenda();
        await Shell.Current.GoToAsync("AgendaPage");
    }

    [RelayCommand]
    private void AddEvent()
    {
        var newEvent = CreateEvent();
        AgendaService.Events.Add(newEvent);
        TcpService.SendEvent(newEvent);
    }

    private AgendaEvent CreateEvent()
    {
        var newEvent = new AgendaEvent
        (
            id: Guid.NewGuid(),
            agendaId: Guid.NewGuid(),
            name: Name,
            location: Location,
            startTime: StartTime,
            endTime: EndTime
        );

        return newEvent;
    }
}