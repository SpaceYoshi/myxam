using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using MyXamClient.Services.Navigation;
using MyXamClient.Services.Tcp;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class EventViewModel(INavigationService navigationService, ITcpService tcpService) : ObservableObject
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
    public async Task GoBack()
    {
        await AgendaViewModel.UpdateAgenda();
        await navigationService.NavigateAsync("AgendaPage");
    }

    [RelayCommand]
    public void AddEvent()
    {
        var newEvent = CreateEvent();
        AgendaService.Events.Add(newEvent);
        tcpService.SendEvent(newEvent);
    }

    public AgendaEvent CreateEvent()
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